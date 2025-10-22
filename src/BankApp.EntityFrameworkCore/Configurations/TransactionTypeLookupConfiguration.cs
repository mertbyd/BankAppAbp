using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankApp.Domain.Lookups;
using Volo.Abp.EntityFrameworkCore.Modeling;
namespace BankApp.EntityFrameworkCore.Configurations;

public class TransactionTypeLookupConfiguration : IEntityTypeConfiguration<TransactionTypeLookup>
{
    public void Configure(EntityTypeBuilder<TransactionTypeLookup> builder)
    {
        builder.ToTable(BankAppDbProperties.DbTablePrefix + "TransactionTypeLookups", 
            BankAppDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.Description)
            .HasMaxLength(200);
        builder.HasIndex(x => x.Type).IsUnique();
    }
}