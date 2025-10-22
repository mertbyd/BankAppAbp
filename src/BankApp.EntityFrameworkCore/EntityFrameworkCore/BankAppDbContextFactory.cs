
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure; 
using Volo.Abp.EntityFrameworkCore.PostgreSql; 
using Volo.Abp;

namespace BankApp.EntityFrameworkCore
{
    public class BankAppDbContextFactory : IDesignTimeDbContextFactory<BankAppDbContext>
    {
        public BankAppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = BuildConfiguration();
            
            // DbContextOptionsBuilder<BankAppDbContext> türünü kullanarak belirsizliği gideriyoruz.
            var builder = new DbContextOptionsBuilder<BankAppDbContext>();
            // PostgreSQL UseNpgsql metodu çağrılıyor.
            builder.UseNpgsql(
                configuration.GetConnectionString("Default"),
                o => o.MigrationsAssembly(typeof(BankAppDbContextFactory).Assembly.FullName)
            );

            return new BankAppDbContext(builder.Options);
        }
        private static IConfigurationRoot BuildConfiguration()
        {
            // Bu metod, appsettings.json dosyasını doğru yerden bulur.
            // Önce DbMigrator projesini dener, bu en doğru yoldur.
            var dbMigratorPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "host", "BankApp.DbMigrator");

            // Eğer DbMigrator yoksa, HttpApi.Host projesini dener.
            var hostPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "host", "BankApp.HttpApi.Host");
            
            string basePath;

            if (Directory.Exists(dbMigratorPath))
            {
                basePath = dbMigratorPath;
            }
            else if (Directory.Exists(hostPath))
            {
                basePath = hostPath;
            }
            else
            {
                throw new UserFriendlyException("Cannot find 'appsettings.json'. Ensure you run 'dotnet ef' from the EntityFrameworkCore project directory and the Host/DbMigrator projects are present.");
            }
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false); 
            return builder.Build();
        }
    }
}
