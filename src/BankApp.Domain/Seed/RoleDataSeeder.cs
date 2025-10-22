    // BankApp.Domain/Data/RoleDataSeeder.cs
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Identity;
    using Volo.Abp.PermissionManagement;
    using Volo.Abp.Uow;
    using BankApp.Constants;
    using BankApp.Permissions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Volo.Abp.Authorization.Permissions;

    namespace BankApp.Data;

    public class RoleDataSeeder : IDataSeeder, ITransientDependency 
    {
        private readonly IIdentityRoleRepository _roleRepository;
        private readonly IPermissionManager _permissionManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ILookupNormalizer _lookupNormalizer;
        private readonly ILogger<RoleDataSeeder> _logger;

        public RoleDataSeeder(
            IIdentityRoleRepository roleRepository,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ILookupNormalizer lookupNormalizer,
            ILogger<RoleDataSeeder> logger)
        {
            _roleRepository = roleRepository;
            _permissionManager = permissionManager;
            _unitOfWorkManager = unitOfWorkManager;
            _lookupNormalizer = lookupNormalizer;
            _logger = logger;
        }

        public async Task SeedAsync(DataSeedContext context = null) 
        {
            using (var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true))
            {
                await SeedRolesAsync();
                await AssignPermissionsToRolesAsync();
                await uow.CompleteAsync();
            }
        }

        private async Task SeedRolesAsync()
        {
            await CreateRoleIfNotExistsAsync(RoleConstants.Admin);
            await CreateRoleIfNotExistsAsync(RoleConstants.Customer);
            await CreateRoleIfNotExistsAsync(RoleConstants.Teller);
            await CreateRoleIfNotExistsAsync(RoleConstants.BranchManager);
            
            _logger.LogInformation($"Roles seeded successfully!");
        }
        private async Task CreateRoleIfNotExistsAsync(string roleName)
        {
            var normalizedRoleName = _lookupNormalizer.NormalizeName(roleName);
            var existingRole = await _roleRepository.FindByNormalizedNameAsync(normalizedRoleName);
            
            if (existingRole == null)
            {
                var role = new IdentityRole(
                    Guid.NewGuid(),
                    roleName,
                    tenantId: null
                )
                {
                    IsStatic = true,
                    IsPublic = true
                };
                
                await _roleRepository.InsertAsync(role, autoSave: false);
                _logger.LogInformation($"Role '{roleName}' created successfully!");
            }
            else
            {
                _logger.LogInformation($"Role '{roleName}' already exists.");
            }
        }
        private async Task AssignPermissionsToRolesAsync()
        {
            await GrantPermissionsToRoleAsync(RoleConstants.Admin, GetAdminPermissions());
            await GrantPermissionsToRoleAsync(RoleConstants.Customer, GetCustomerPermissions());
            await GrantPermissionsToRoleAsync(RoleConstants.Teller, GetTellerPermissions());
            await GrantPermissionsToRoleAsync(RoleConstants.BranchManager, GetBranchManagerPermissions());
        }
        private async Task GrantPermissionsToRoleAsync(string roleName, string[] permissions)
        {
            var grantedCount = 0;
            foreach (var permission in permissions)
            {
                try
                {
                    var permissionGrant = await _permissionManager.GetAsync(
                        permission, 
                            RolePermissionValueProvider.ProviderName, 
                        roleName
                    );
                    
                    if (permissionGrant == null || !permissionGrant.IsGranted)
                    {
                        await _permissionManager.SetAsync(
                            permission,
                            RolePermissionValueProvider.ProviderName,
                            roleName,
                            true
                        );
                        grantedCount++;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Permission '{permission}' could not be granted to role '{roleName}': {ex.Message}");
                }
            }
            
            _logger.LogInformation($"Granted {grantedCount} permissions to role '{roleName}'");
        }
        private string[] GetAdminPermissions()
        {
            return new[]
            {
                BankAppPermissions.Customer.Default,
                BankAppPermissions.Customer.Read,
                BankAppPermissions.Customer.Write,
                BankAppPermissions.Customer.Delete,
                BankAppPermissions.Customer.ViewAll,
                BankAppPermissions.Account.Default,
                BankAppPermissions.Account.Read,
                BankAppPermissions.Account.Write,
                BankAppPermissions.Account.Delete,
                BankAppPermissions.Account.Transfer,
                BankAppPermissions.Card.Default,
                BankAppPermissions.Card.Read,
                BankAppPermissions.Card.Write,
                BankAppPermissions.Card.Delete,
                BankAppPermissions.Card.Purchase,
                BankAppPermissions.Card.Withdraw,
                BankAppPermissions.Card.Deposit,
                BankAppPermissions.Transaction.Default,
                BankAppPermissions.Transaction.Read,
                BankAppPermissions.Transaction.Write,
                BankAppPermissions.Transaction.ViewAll,
                BankAppPermissions.RoleManagement.Default,
                BankAppPermissions.RoleManagement.Create,
                BankAppPermissions.RoleManagement.Update,
                BankAppPermissions.RoleManagement.Delete,
                BankAppPermissions.RoleManagement.ViewAll,
                BankAppPermissions.RoleManagement.ManagePermissions,
                BankAppPermissions.UserManagement.Default,
                BankAppPermissions.UserManagement.Create,
                BankAppPermissions.UserManagement.Update,
                BankAppPermissions.UserManagement.Delete,
                BankAppPermissions.UserManagement.ViewAll,
                BankAppPermissions.UserManagement.ManageRoles,
                BankAppPermissions.UserManagement.ManagePermissions,
                BankAppPermissions.PermissionManagement.Default,
                BankAppPermissions.PermissionManagement.Grant,
                BankAppPermissions.PermissionManagement.Revoke,
                BankAppPermissions.PermissionManagement.ViewAll
            };
        }

        private string[] GetCustomerPermissions()
        {
            return new[]
            {
                BankAppPermissions.Account.Default,
                BankAppPermissions.Account.Read,
                BankAppPermissions.Account.Write,
                BankAppPermissions.Account.Transfer,
                
                BankAppPermissions.Card.Default,
                BankAppPermissions.Card.Read,
                BankAppPermissions.Card.Write,
                BankAppPermissions.Card.Delete,
                BankAppPermissions.Card.Purchase,
                BankAppPermissions.Card.Withdraw,
                BankAppPermissions.Card.Deposit,
                
                BankAppPermissions.Transaction.Default,
                BankAppPermissions.Transaction.Read,
                BankAppPermissions.Transaction.Write
            };
        }

        private string[] GetTellerPermissions()
        {
            return new[]
            {
                BankAppPermissions.Customer.Default,
                BankAppPermissions.Customer.Read,
                BankAppPermissions.Customer.Write,
                
                BankAppPermissions.Account.Default,
                BankAppPermissions.Account.Read,
                BankAppPermissions.Account.Write,
                BankAppPermissions.Account.Transfer,
                
                BankAppPermissions.Card.Default,
                BankAppPermissions.Card.Read,
                BankAppPermissions.Card.Write,
                BankAppPermissions.Card.Withdraw,
                BankAppPermissions.Card.Deposit,
                
                BankAppPermissions.Transaction.Default,
                BankAppPermissions.Transaction.Read,
                BankAppPermissions.Transaction.Write
            };
        }

        private string[] GetBranchManagerPermissions()
        {
            return new[]
            {
                BankAppPermissions.Customer.Default,
                BankAppPermissions.Customer.Read,
                BankAppPermissions.Customer.Write,
                BankAppPermissions.Customer.Delete,
                BankAppPermissions.Customer.ViewAll,
                
                BankAppPermissions.Account.Default,
                BankAppPermissions.Account.Read,
                BankAppPermissions.Account.Write,
                BankAppPermissions.Account.Delete,
                BankAppPermissions.Account.Transfer,
                
                BankAppPermissions.Card.Default,
                BankAppPermissions.Card.Read,
                BankAppPermissions.Card.Write,
                BankAppPermissions.Card.Delete,
                BankAppPermissions.Card.Purchase,
                BankAppPermissions.Card.Withdraw,
                BankAppPermissions.Card.Deposit,
                
                BankAppPermissions.Transaction.Default,
                BankAppPermissions.Transaction.Read,
                BankAppPermissions.Transaction.Write,
                BankAppPermissions.Transaction.ViewAll,
                
                BankAppPermissions.UserManagement.Default,
                BankAppPermissions.UserManagement.Create,
                BankAppPermissions.UserManagement.Update,
                BankAppPermissions.UserManagement.ViewAll
            };
        }
    }