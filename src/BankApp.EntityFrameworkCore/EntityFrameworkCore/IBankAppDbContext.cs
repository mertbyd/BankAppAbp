using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace BankApp.EntityFrameworkCore;

[ConnectionStringName(BankAppDbProperties.ConnectionStringName)]
public interface IBankAppDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
