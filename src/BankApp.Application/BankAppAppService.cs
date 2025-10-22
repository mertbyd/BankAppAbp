using System;
using System.Threading.Tasks;
using System.Linq;
using BankApp.Localization;
using Volo.Abp.Application.Services;
using BankApp.Interface;
using Volo.Abp;

namespace BankApp;

public abstract class BankAppAppService : ApplicationService
{
    protected readonly ICustomerRepository CustomerRepository;
    
    // Parametresiz constructor (geriye dönük uyumluluk için)
    protected BankAppAppService()
    {
        LocalizationResource = typeof(BankAppResource);
        ObjectMapperContext = typeof(BankAppApplicationModule);
    }
    
    // CustomerRepository ile constructor
    protected BankAppAppService(ICustomerRepository customerRepository) : this()
    {
        CustomerRepository = customerRepository;
    }
    
    // Current user"ın CustomerId"sini getir
    protected async Task<Guid> GetCurrentUserCustomerIdAsync()
    {
        var userId = CurrentUser.Id;
        if (!userId.HasValue)
            throw new BusinessException(BankAppDomainErrorCodes.Auth.InvalidToken)
                .WithData("Reason", "UserNotAuthenticated");
        if (CustomerRepository == null)
            throw new BusinessException(BankAppDomainErrorCodes.General.UnknownError)
                .WithData("Reason", "CustomerRepositoryNotInjected");
        var customers = await CustomerRepository.GetByUserIdAsync(userId.Value);
        if (customers == null || customers.Count == 0)
        {
            throw new BusinessException(BankAppDomainErrorCodes.Auth.UserNotAssociatedWithCustomer)
                .WithData("UserId", userId.Value);
        }
        
        return customers.First().Id;
    }
    
    // Current user"ın UserId"sini getir
    protected Guid GetCurrentUserId()
    {
        if (!CurrentUser.Id.HasValue)
            throw new BusinessException(BankAppDomainErrorCodes.Auth.InvalidToken)
                .WithData("Reason", "UserNotAuthenticated");
        return CurrentUser.Id.Value;
    }
}