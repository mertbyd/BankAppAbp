using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace BankApp;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BankAppHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class BankAppConsoleApiClientModule : AbpModule
{

}
