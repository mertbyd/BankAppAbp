using Localization.Resources.AbpUi;
using BankApp.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace BankApp;

[DependsOn(
    typeof(BankAppApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class BankAppHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(BankAppHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<BankAppResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
