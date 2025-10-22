using BankApp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace BankApp;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(BankAppEntityFrameworkCoreTestModule)
    )]
public class BankAppDomainTestModule : AbpModule
{

}
