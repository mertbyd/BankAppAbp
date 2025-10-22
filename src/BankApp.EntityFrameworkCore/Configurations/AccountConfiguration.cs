using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankApp.Entities;

namespace BankApp.EntityFrameworkCore.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(BankAppDbProperties.DbTablePrefix + "Accounts", 
            BankAppDbProperties.DbSchema);
        builder.Property(x => x.AccountName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.UserId).IsRequired();
        builder.HasIndex(x => x.UserId);
        builder.Property(x => x.AccountNumber)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.IBAN)
            .IsRequired()
            .HasMaxLength(26);
        builder.Property(x => x.CustomerId)
            .IsRequired();
        builder.Property(x => x.OpenedAt)
            .IsRequired();
        builder.Property(x => x.Balance)
            .HasPrecision(15, 2)
            .HasDefaultValue(0);
        builder.HasIndex(x => x.AccountNumber).IsUnique();
        builder.HasIndex(x => x.IBAN).IsUnique();
        builder.HasIndex(x => x.CustomerId);
    }
}