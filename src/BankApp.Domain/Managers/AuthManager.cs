// Managers/AuthManager.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
using AutoMapper;
using BankApp.Models.Auth;
using Volo.Abp.Identity;
using BankApp.Constants;
using Volo.Abp;
using Volo.Abp.Data;

namespace BankApp.Managers;

public class AuthManager : DomainService
{
    private readonly IdentityUserManager _identityUserManager;
    private readonly IIdentityUserRepository _identityUserRepository;
    private readonly IIdentityRoleRepository _roleRepository;
    private readonly IMapper _mapper;
    private readonly IdentityRoleManager _identityRoleManager;
    private readonly ILogger<AuthManager> _logger;
    
    public AuthManager(
        IdentityUserManager identityUserManager,
        IIdentityUserRepository identityUserRepository,
        IIdentityRoleRepository roleRepository,
        IdentityRoleManager identityRoleManager,
        ILogger<AuthManager> logger,
        IMapper mapper)
    {
        _identityUserManager = identityUserManager;
        _identityUserRepository = identityUserRepository;
        _roleRepository = roleRepository;
        _logger = logger;
        _mapper = mapper;
        _identityRoleManager = identityRoleManager;
    }
    public async Task<IdentityUser> CreateUserAsync(RegisterModel model)
    {
        
        await ValidateEmailUniquenessAsync(model.Email);
        await ValidateUsernameUniquenessAsync(model.UserName);
        await ValidateRoleNameAsync(model.RoleName);
        // IdentityUser oluştur (Mapper kullanarak)
        var user = _mapper.Map<IdentityUser>(
            model, 
            opts => opts.Items["GuidGenerator"] = GuidGenerator
        );
        // Telefon numarası set et (mapping'de yapılıyor ama kontrol için)
        if (!string.IsNullOrWhiteSpace(model.PhoneNumber) && string.IsNullOrWhiteSpace(user.PhoneNumber))
            user.SetPhoneNumber(model.PhoneNumber, false);
        // Password hash"le ve user"ı oluştur
        var result = await _identityUserManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BusinessException(BankAppDomainErrorCodes.General.UnknownError);
        }
        // Role ata
        var roleName = !string.IsNullOrWhiteSpace(model.RoleName) 
            ? model.RoleName 
            : RoleConstants.DefaultRoleName;
        await _identityUserManager.AddToRoleAsync(user, roleName);
        return user;
    }
    
    public async Task<IdentityUser> ValidateLoginAsync(LoginModel model)
    {
        var user = await _identityUserManager.FindByNameAsync(model.UserName);
        if (user == null)
            throw new BusinessException(BankAppDomainErrorCodes.Auth.InvalidCredentials);
        var isPasswordValid = await _identityUserManager.CheckPasswordAsync(user, model.Password);
        if (!isPasswordValid)
            throw new BusinessException(BankAppDomainErrorCodes.Auth.InvalidCredentials);
        return user;
    }
    
    #region Private Validation Methods
    private async Task ValidateEmailUniquenessAsync(string email)
    {
        var existingUser = await _identityUserManager.FindByEmailAsync(email);
        if (existingUser != null)
            throw new BusinessException(BankAppDomainErrorCodes.General.ValidationFailed);
    }
    private async Task ValidateUsernameUniquenessAsync(string username)
    {
        var existingUser = await _identityUserManager.FindByNameAsync(username);
        if (existingUser != null)
            throw new BusinessException(BankAppDomainErrorCodes.General.ValidationFailed);
    }
    private async Task ValidateRoleNameAsync(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return;
        var roleExists = await _identityRoleManager.RoleExistsAsync(roleName);
        if (!roleExists)
            throw new BusinessException(BankAppDomainErrorCodes.General.ValidationFailed);
    }
    
    #endregion
}