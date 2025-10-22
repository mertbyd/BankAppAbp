using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankApp.Entities;
using BankApp.Domain.Lookups;
namespace BankApp.EntityFrameworkCore.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(BankAppDbProperties.DbTablePrefix + "Transactions", 
            BankAppDbProperties.DbSchema);
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.Amount).HasPrecision(15, 2).IsRequired();
        builder.Property(x => x.TransactionDate).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.TransactionTypesId)
            .HasConversion<int>()
            .IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.HasIndex(x => x.UserId);
        builder.Property(x => x.TransactionTypesId)
            .HasConversion<int>()
            .HasColumnName("TransactionTypeId")
            .IsRequired();
        builder.HasOne<TransactionTypeLookup>()
            .WithMany()
            .HasForeignKey("TransactionTypeId")
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => x.CustomerId);        
        builder.HasIndex(x => x.CardId);            
        builder.HasIndex(x => x.SourceAccountId);   
        builder.HasIndex(x => x.TargetAccountId);   
        builder.HasIndex(x => x.TransactionDate);   
        builder.HasIndex(x => x.TransactionTypesId); 
    }
}