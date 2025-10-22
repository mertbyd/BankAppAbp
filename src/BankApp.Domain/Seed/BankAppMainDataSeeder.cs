// BankApp.Domain/Data/BankAppMainDataSeeder.cs
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace BankApp.Data;
public class BankAppMainDataSeeder : IDataSeeder, ITransientDependency
{
    private readonly BankAppDataSeeder _lookupSeeder;
    private readonly RoleDataSeeder _roleSeeder;
    private readonly ILogger<BankAppMainDataSeeder> _logger;
    public BankAppMainDataSeeder(
        BankAppDataSeeder lookupSeeder,
        RoleDataSeeder roleSeeder,
        ILogger<BankAppMainDataSeeder> logger)
    {
        _lookupSeeder = lookupSeeder;
        _roleSeeder = roleSeeder;
        _logger = logger;
    }
    public async Task SeedAsync(DataSeedContext context = null)
    {
        context ??= new DataSeedContext();
        
        try
        {
            
            await _lookupSeeder.SeedAsync(context);
            await _roleSeeder.SeedAsync(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during data seeding!");
            throw;
        }
    }
}