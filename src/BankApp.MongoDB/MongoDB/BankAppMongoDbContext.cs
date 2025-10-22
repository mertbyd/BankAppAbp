using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace BankApp.MongoDB;

[ConnectionStringName(BankAppDbProperties.ConnectionStringName)]
public class BankAppMongoDbContext : AbpMongoDbContext, IBankAppMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureBankApp();
    }
}
