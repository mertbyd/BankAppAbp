using Volo.Abp;
using Volo.Abp.MongoDB;

namespace BankApp.MongoDB;

public static class BankAppMongoDbContextExtensions
{
    public static void ConfigureBankApp(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
