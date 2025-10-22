using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace BankApp.MongoDB;

[ConnectionStringName(BankAppDbProperties.ConnectionStringName)]
public interface IBankAppMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
