using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace BankApp;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class BankAppInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BankAppInstallerModule>();
        });
    }
}
