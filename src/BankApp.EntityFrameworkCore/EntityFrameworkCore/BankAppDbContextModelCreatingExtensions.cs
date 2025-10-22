using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using System.Reflection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;

using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace BankApp.EntityFrameworkCore;

public static class BankAppDbContextModelCreatingExtensions
{
    public static void ConfigureBankApp(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(BankAppDbProperties.DbTablePrefix + "Questions", BankAppDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
        Check.NotNull(builder, nameof(builder));

        // 1. ABP MODÜL VARLIKLARINI DAHİL ETMEK
        // Bu, JSON ile ayarlanan şemaların çalışması için kritik:
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        // 2. KENDİ VARLIK KONFİGÜRASYONLARIMIZI UYGULAMA (SİZİN KODUNUZ)
        // Bu satır, sizin yazdığınız tüm Configuration sınıflarını (Account, Card, Lookup) otomatik bulur ve uygular.
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
