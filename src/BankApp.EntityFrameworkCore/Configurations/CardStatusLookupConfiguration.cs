using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankApp.Domain.Lookups;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace BankApp.EntityFrameworkCore.Configurations;

public class CardStatusLookupConfiguration : IEntityTypeConfiguration<CardStatusLookup>
{
    public void Configure(EntityTypeBuilder<CardStatusLookup> builder)
    {
        builder.ToTable(BankAppDbProperties.DbTablePrefix + "CardStatusLookups", 
            BankAppDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.Description)
            .HasMaxLength(200);
        builder.HasIndex(x => x.Status).IsUnique();
    }
}