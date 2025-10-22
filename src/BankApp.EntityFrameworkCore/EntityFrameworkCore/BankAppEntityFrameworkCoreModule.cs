using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace BankApp.EntityFrameworkCore;

[DependsOn(
    typeof(BankAppDomainModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpAutoMapperModule), 
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
)]
public class BankAppEntityFrameworkCoreModule : AbpModule
{
    static BankAppEntityFrameworkCoreModule()
    {
        ConfigureDbSchemasStatic();
    }
    
    private static void ConfigureDbSchemasStatic()
    {
        // ABP modülleri için schema ayarları
        Volo.Abp.Identity.AbpIdentityDbProperties.DbSchema = "abp";
        Volo.Abp.PermissionManagement.AbpPermissionManagementDbProperties.DbSchema = "abp";
        Volo.Abp.SettingManagement.AbpSettingManagementDbProperties.DbSchema = "abp";
        Volo.Abp.AuditLogging.AbpAuditLoggingDbProperties.DbSchema = "abp";
        Volo.Abp.BackgroundJobs.AbpBackgroundJobsDbProperties.DbSchema = "abp";
        Volo.Abp.FeatureManagement.AbpFeatureManagementDbProperties.DbSchema = "abp";
        Volo.Abp.TenantManagement.AbpTenantManagementDbProperties.DbSchema = "abp";
        Volo.Abp.OpenIddict.AbpOpenIddictDbProperties.DbSchema = "openiddict";
        BankAppDbProperties.DbSchema = "bankapp";
    }
    
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        ConfigureDbSchemas(configuration);
    }
    
    private void ConfigureDbSchemas(IConfiguration configuration)
    {
        ConfigureDbSchemasStatic();
        var schemas = configuration.GetSection("EntityFrameworkCore:Schemas");
        if (schemas.Exists())
        {
            var identitySchema = schemas["Volo.Abp.Identity"];
            if (!string.IsNullOrEmpty(identitySchema))
                Volo.Abp.Identity.AbpIdentityDbProperties.DbSchema = identitySchema;
            var permissionSchema = schemas["Volo.Abp.PermissionManagement"];
            if (!string.IsNullOrEmpty(permissionSchema))
                Volo.Abp.PermissionManagement.AbpPermissionManagementDbProperties.DbSchema = permissionSchema;
            var settingSchema = schemas["Volo.Abp.SettingManagement"];
            if (!string.IsNullOrEmpty(settingSchema))
                Volo.Abp.SettingManagement.AbpSettingManagementDbProperties.DbSchema = settingSchema;
            var auditSchema = schemas["Volo.Abp.AuditLogging"];
            if (!string.IsNullOrEmpty(auditSchema))
                Volo.Abp.AuditLogging.AbpAuditLoggingDbProperties.DbSchema = auditSchema;
            var openIddictSchema = schemas["Volo.Abp.OpenIddict"];
            if (!string.IsNullOrEmpty(openIddictSchema))
                Volo.Abp.OpenIddict.AbpOpenIddictDbProperties.DbSchema = openIddictSchema;
            var tenantSchema = schemas["Volo.Abp.TenantManagement"];
            if (!string.IsNullOrEmpty(tenantSchema))
                Volo.Abp.TenantManagement.AbpTenantManagementDbProperties.DbSchema = tenantSchema;
            var bgJobsSchema = schemas["Volo.Abp.BackgroundJobs"];
            if (!string.IsNullOrEmpty(bgJobsSchema))
                Volo.Abp.BackgroundJobs.AbpBackgroundJobsDbProperties.DbSchema = bgJobsSchema;
            var featureSchema = schemas["Volo.Abp.FeatureManagement"];
            if (!string.IsNullOrEmpty(featureSchema))
                Volo.Abp.FeatureManagement.AbpFeatureManagementDbProperties.DbSchema = featureSchema;
            var bankAppSchema = schemas["BankApp"];
            if (!string.IsNullOrEmpty(bankAppSchema))
                BankAppDbProperties.DbSchema = bankAppSchema;
        }
    }
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseNpgsql();
        });
        
        context.Services.AddAbpDbContext<BankAppDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
        });
    }
}