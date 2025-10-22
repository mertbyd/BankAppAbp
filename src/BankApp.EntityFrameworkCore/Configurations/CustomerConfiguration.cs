using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankApp.Entities;

namespace BankApp.EntityFrameworkCore.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(BankAppDbProperties.DbTablePrefix + "Customers", 
            BankAppDbProperties.DbSchema);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.TcNumber).IsRequired().HasMaxLength(11);
        builder.Property(x => x.BirthPlace).HasMaxLength(100);
        builder.Property(x => x.PhoneNumber).HasMaxLength(20);
        builder.Property(x => x.Email).HasMaxLength(100);
        builder.Property(x => x.Address).HasMaxLength(500);
        builder.Property(x => x.RiskLimit).HasPrecision(15, 2).HasDefaultValue(10000.00m);
        builder.Property(x => x.UserId).IsRequired(); 
        builder.HasIndex(x => x.TcNumber).IsUnique();
        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.UserId); 
    }
}

