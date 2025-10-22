using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankApp.Entities;
using BankApp.Domain.Lookups;
namespace BankApp.EntityFrameworkCore.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable(BankAppDbProperties.DbTablePrefix + "Cards", 
            BankAppDbProperties.DbSchema);
        // UserId property configuration
        builder.Property(x => x.UserId).IsRequired();
        builder.HasIndex(x => x.UserId);
        builder.Property(x => x.CardNumber).IsRequired().HasMaxLength(16);
        builder.Property(x => x.ExpiryYear).IsRequired();
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.CreditLimit).HasPrecision(15, 2);
        builder.Property(x => x.AvailableLimit).HasPrecision(15, 2);
        builder.Property(x => x.UsedLimit).HasPrecision(15, 2).HasDefaultValue(0); 
        builder.Property(x => x.CardType)
            .HasConversion<int>()
            .IsRequired();
        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();
        builder.HasIndex(x => x.CardNumber).IsUnique(); 
        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.AccountId);
    }
}