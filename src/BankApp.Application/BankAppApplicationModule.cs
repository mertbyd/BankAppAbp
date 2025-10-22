
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace BankApp;

[DependsOn(
    typeof(BankAppDomainModule),
    typeof(BankAppApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
)]
public class BankAppApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        
        // HttpClient
        services.AddHttpClient();
        
        services.AddAutoMapper(typeof(BankAppApplicationModule).Assembly);
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BankAppApplicationModule>(validate: true);
        });
        
        // ABP ObjectMapper registration
        context.Services.AddAutoMapperObjectMapper<BankAppApplicationModule>();
    }
}