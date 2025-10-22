using BankApp.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace BankApp;
[RemoteService(Name = "Default", IsEnabled = true)] // IsEnabled = true önemli!
[Area("app")] // Area ekle
[Route("api/app/[controller]")] // Route"u düzelt
public abstract class BankAppController : AbpController
{
    protected BankAppController()
    {
        LocalizationResource = typeof(BankAppResource);
    }
}
