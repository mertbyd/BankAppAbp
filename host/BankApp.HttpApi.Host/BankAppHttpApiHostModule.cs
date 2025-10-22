using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using BankApp.EntityFrameworkCore;
using BankApp.MultiTenancy;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using OpenIddict.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.Emailing;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Auditing;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using BankApp;
using Microsoft.AspNetCore.Authentication;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Bankproject.ApiResponse.Extensions; // ✅ API RESPONSE WRAPPER USING

namespace BankApp;
[DependsOn(
    // Core Application Modules
    typeof(BankAppApplicationModule),
    typeof(BankAppEntityFrameworkCoreModule),
    typeof(BankAppHttpApiModule),
    // Account & Authentication Modules
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAccountHttpApiModule),
    // OpenIddict Modules
    typeof(AbpOpenIddictAspNetCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpSettingManagementApplicationModule), 
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    // Identity Modules
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpIdentityHttpApiModule),
    // Permission Management
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpPermissionManagementHttpApiModule),
    // Setting Management
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    // Feature Management
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementHttpApiModule),
    // Tenant Management
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpTenantManagementHttpApiModule),
    // UI & Infrastructure
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpAutofacModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule)
)]
public class BankAppHttpApiHostModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("BankApp");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });
    }
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // API"ler için antiforgery"yi devre dışı bırak
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            // veya tüm controller"lar için
            options.ConventionalControllers.Create(typeof(BankAppApplicationModule).Assembly, opts =>
            {
                opts.RootPath = "api";
            });
        });
        Configure<AbpAntiForgeryOptions>(options =>
        {
            options.AutoValidate = false; // Tamamen kapat
        });
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        
        // SADECE OpenIddict kullan, JWT KALDIR
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        // PostgreSQL için
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        context.Services.Configure<AbpDbContextOptions>(options =>
        {
            options.UseNpgsql();
        });
        
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<BankAppDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                    string.Format("..{0}..{0}src{0}BankApp.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<BankAppDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                    string.Format("..{0}..{0}src{0}BankApp.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<BankAppApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                    string.Format("..{0}..{0}src{0}BankApp.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<BankAppApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                    string.Format("..{0}..{0}src{0}BankApp.Application", Path.DirectorySeparatorChar)));
            });
        }
        // Swagger configuration - Sadece Bearer token
        context.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "BankApp API", Version = "v1" });
            options.DocInclusionPredicate((docName, description) => true);
            options.CustomSchemaIds(type => type.FullName);
            // Bearer token authentication
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Token girin (Bearer prefixi otomatik eklenir)"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme, 
                            Id = "Bearer" 
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
        });
        Configure<AbpAuditingOptions>(options =>
        {
            options.ApplicationName = "BankApp";
        });
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"] ?? "https://localhost:44327";
        });
        // JWT Authentication KALDIRILDI - Sadece OpenIddict kullanılıyor
        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "BankApp:";
        });
        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("BankApp");
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        #if DEBUG
        context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
        #endif
        context.Services.AddApiResponseWrapper(options =>
        {
            options.UseIndentedJson = false;
            options.ShowDetailedErrors = hostingEnvironment.IsDevelopment();
            options.ApiPathPrefix = "/api";
            options.LogExceptions = true;
            options.ExcludePaths = new List<string> 
            { 
                "/swagger",
                "/health",
                "/account",
                "/connect",  
                "/abp"
            };
        });
    }
    public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();
        
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseErrorPage();
            app.UseHsts();
        }
        
        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();
        
        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }
        
        app.UseAbpRequestLocalization();
        app.UseAuthorization();
        app.UseApiResponseWrapper();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "BankApp API v1");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
        // Seed data
        await SeedData(context);
        await SeedOpenIddictAsync(context);
    }
    
    private async Task SeedOpenIddictAsync(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            try
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<BankAppHttpApiHostModule>>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
                var scopeManager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();
                var permissionDataSeeder = scope.ServiceProvider.GetRequiredService<IPermissionDataSeeder>();
                var localizer = scope.ServiceProvider.GetRequiredService<IStringLocalizer<OpenIddictResponse>>(); 
                var seederLogger = scope.ServiceProvider.GetRequiredService<ILogger<OpenIddictDataSeedContributor>>(); 
                
                var openIddictSeeder = new OpenIddictDataSeedContributor(
                    configuration,
                    applicationManager,
                    scopeManager,
                    permissionDataSeeder,
                    localizer,
                    seederLogger
                );
                await openIddictSeeder.SeedAsync(new DataSeedContext());
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<BankAppHttpApiHostModule>>();
            }
        }
    }
    
    private async Task SeedData(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            await scope.ServiceProvider
                .GetRequiredService<IDataSeeder>()
                .SeedAsync();
        }
    }
}