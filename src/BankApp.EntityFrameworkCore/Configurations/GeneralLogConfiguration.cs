using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankApp.Entities;

namespace BankApp.EntityFrameworkCore.Configurations;

public class GeneralLogConfiguration : IEntityTypeConfiguration<GeneralLog>
{
    public void Configure(EntityTypeBuilder<GeneralLog> builder)
    {
        builder.ToTable(BankAppDbProperties.DbTablePrefix + "GeneralLogs", 
            BankAppDbProperties.DbSchema);
        builder.Property(x => x.Amount)
            .HasPrecision(15, 2);  
        builder.HasIndex(x => x.CreationTime);
    }
}