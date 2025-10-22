using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Volo.Abp.Data; // ✅ EKLE

namespace BankApp;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting web host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();

            await builder.AddApplicationAsync<BankAppHttpApiHostModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();

            // ✅ SEED KONTROLÜ EKLE
            if (args.Length > 0 && args[0] == "--seed")
            {
                Log.Information("Running data seeder...");
                using (var scope = app.Services.CreateScope())
                {
                    var dataSeeder = scope.ServiceProvider
                        .GetRequiredService<IDataSeeder>();
                    
                    await dataSeeder.SeedAsync();
                }
                Log.Information("Data seeding completed successfully!");
                return 0; // Seed tamamlandı, uygulamayı başlatma
            }
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}