// BankApp.EntityFrameworkCore/AuthServerDbContextFactory.cs (veya bulunduğu yer)

using System.IO;
using BankApp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
// KRİTİK: PostgreSQL using'i
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure; 

public class AuthServerDbContextFactory : IDesignTimeDbContextFactory<AuthServerDbContext>
{
    public AuthServerDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<AuthServerDbContext>()
            // BURADA SQL Server yerine PostgreSQL kullanılacak:
            .UseNpgsql(configuration.GetConnectionString("Default")); // UseNpgsql olmalı!

        return new AuthServerDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "host", "BankApp.AuthServer");

        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}