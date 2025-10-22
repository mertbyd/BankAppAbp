
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
        
        // AutoMapper assembly taraması
        services.AddAutoMapper(typeof(BankAppApplicationModule).Assembly);
        
        // ABP AutoMapper configuration - SADECE BİR KEZ!
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BankAppApplicationModule>(validate: false); // false
        });
        
        // ABP ObjectMapper registration
        context.Services.AddAutoMapperObjectMapper<BankAppApplicationModule>();
    }
}