using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using BankApp.Domain.Lookups;
using BankApp.Entities;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
// EKLENMESİ GEREKENLER (Sembolleri Çözmek İçin):
using Volo.Abp.Identity.EntityFrameworkCore;           
using Volo.Abp.PermissionManagement.EntityFrameworkCore; 
using Volo.Abp.SettingManagement.EntityFrameworkCore;   
using Volo.Abp.AuditLogging.EntityFrameworkCore;        
using Volo.Abp.OpenIddict.EntityFrameworkCore;          

using System.Reflection;
using Volo.Abp.Identity;

namespace BankApp.EntityFrameworkCore;

[ConnectionStringName(BankAppDbProperties.ConnectionStringName)]
public class BankAppDbContext : AbpDbContext<BankAppDbContext>, IBankAppDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<CardStatusLookup> CardStatusLookups { get; set; }
    public DbSet<CardTypeLookup> CardTypeLookups { get; set; }
    public DbSet<TransactionTypeLookup> TransactionTypeLookups { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Account>  Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<GeneralLog> GeneralLogs { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public BankAppDbContext(DbContextOptions<BankAppDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureBankApp();
      
    }
}