// BankApp.EntityFrameworkCore/BankAppHttpApiHostMigrationsDbContextFactory.cs

using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
// PostgreSQL desteği için gerekli using
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure; 

namespace BankApp.EntityFrameworkCore
{
    public class BankAppHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<BankAppHttpApiHostMigrationsDbContext>
    {
        public BankAppHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            var builder = new DbContextOptionsBuilder<BankAppHttpApiHostMigrationsDbContext>()
                .UseNpgsql(configuration.GetConnectionString("Default"), 
                    o => o.MigrationsAssembly(typeof(BankAppHttpApiHostMigrationsDbContextFactory).Assembly.FullName)); // Migrations Assembly'sini ayarla
            return new BankAppHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            // appsettings.json dosyasını Host projesinden bulur.
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "host", "BankApp.HttpApi.Host");
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}