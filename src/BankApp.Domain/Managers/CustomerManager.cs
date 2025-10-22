// BankApp.Domain/Managers/CustomerManager.cs
using System;
using System.Threading.Tasks;
using AutoMapper;
using BankApp.Models.Customers;
using Volo.Abp.Domain.Entities;
using BankApp.Interface;
using BankApp.Entities;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using BankApp;
using System.Linq;
using Volo.Abp.Identity;

namespace BankApp.Managers;

public class CustomerManager : BaseManager<Customer>
{
    private readonly IMapper _mapper;
    public CustomerManager(
        ICustomerRepository customerRepository,
        IIdentityUserRepository userRepository, 
        IMapper mapper
        ) // Logger injection'ı bırakıldı ancak kullanılmayacak
        : base(customerRepository, customerRepository, userRepository)
    {
        _mapper = mapper;
        // _logger = logger; // Atama kaldırıldı
    }
    
    #region Create Customer
    
    public async Task<Customer> CreateAsync(CreateCustomerModel model)
    {
        await ValidateUserExistsAsync(model.UserId);
        var existingCustomers = await CustomerRepository.GetByUserIdAsync(model.UserId);
        if (existingCustomers != null && existingCustomers.Count > 0)
            throw new BusinessException(
                BankAppDomainErrorCodes.General.InvalidOperation); 
        // TC Number benzersizlik kontrolü
        await ValidateTcNumberUniquenessAsync(model.TcNumber);
        // Email benzersizlik kontrolü (opsiyonel alan)
        if (!string.IsNullOrEmpty(model.Email))
            await ValidateEmailUniquenessAsync(model.Email);
        // Entity oluştur
        var customer = _mapper.Map<Customer>(model);
        customer.SetId(GuidGenerator.Create());
        customer.UserId = model.UserId;
        customer.RiskLimit = 10000.00m; 
        return customer;
    }
    
    #endregion
    #region Update Customer
    public async Task<Customer> UpdateAsync(Guid customerId, UpdateCustomerModel model)
    {
        // Customer'ı getir
        var existingCustomer = await CustomerRepository.GetAsync(customerId);
        if (existingCustomer == null)
            throw new BusinessException(BankAppDomainErrorCodes.Customers.NotFound); 
        // Email değişiyorsa uniqueness kontrol et  
        if (!string.IsNullOrEmpty(model.Email) && model.Email != existingCustomer.Email)
            await ValidateEmailUniquenessAsync(model.Email, existingCustomer.Id);
        // Risk limit validasyonu
        ValidateRiskLimit(model.RiskLimit);
        // Mapper ile güncelleme
        _mapper.Map(model, existingCustomer);
        return existingCustomer;
    }
    #endregion
    #region Helper Methods
    // User'ın customer'ı var mı kontrol eder
    public async Task<bool> UserHasCustomerAsync(Guid userId)
    {
        var customers = await CustomerRepository.GetByUserIdAsync(userId);
        return customers != null && customers.Count > 0;
    }
    // User'ın customer'ını getirir
    public async Task<Customer> GetCustomerByUserIdAsync(Guid userId)
    {
        var customers = await CustomerRepository.GetByUserIdAsync(userId);
        if (customers == null || customers.Count == 0)
            throw new BusinessException(BankAppDomainErrorCodes.Customers.NotFound); 
        return customers.First();
    }
    #endregion
    #region Model Validations (REMOVED)
    #endregion
    #region Business Rules
    private async Task ValidateTcNumberUniquenessAsync(string tcNumber)
    {
        var customers = await CustomerRepository.GetListAsync();
        var existingCustomer = customers.FirstOrDefault(x => x.TcNumber == tcNumber);
        if (existingCustomer != null)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation);
    }
    private async Task ValidateEmailUniquenessAsync(string email, Guid? excludeId = null)
    {
        var customers = await CustomerRepository.GetListAsync();
        var existingCustomer = customers.FirstOrDefault(x => 
            x.Email == email && x.Id != excludeId);
        if (existingCustomer != null)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation);
    }
    private void ValidateRiskLimit(decimal riskLimit)
    {
        if (riskLimit < 0)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation); 
        if (riskLimit > 1000000)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation); 
    }
    #endregion
}