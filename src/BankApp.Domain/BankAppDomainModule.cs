using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace BankApp;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(BankAppDomainSharedModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(AbpPermissionManagementDomainModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpObjectExtendingModule)

)]
public class BankAppDomainModule : AbpModule
{

}
