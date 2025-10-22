using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace BankApp;

[DependsOn(
    typeof(BankAppApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class BankAppHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(BankAppApplicationContractsModule).Assembly,
            BankAppRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BankAppHttpApiClientModule>();
        });

    }
}
