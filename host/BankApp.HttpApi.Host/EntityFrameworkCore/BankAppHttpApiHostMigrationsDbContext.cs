using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BankApp.EntityFrameworkCore;

public class BankAppHttpApiHostMigrationsDbContext : AbpDbContext<BankAppHttpApiHostMigrationsDbContext>
{
    public BankAppHttpApiHostMigrationsDbContext(DbContextOptions<BankAppHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureBankApp();
    }
}
