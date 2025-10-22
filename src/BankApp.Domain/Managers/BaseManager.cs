// BankApp.Domain/Managers/BaseManager.cs
using System;
using System.Threading.Tasks;
using BankApp.Interface;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace BankApp.Managers;

public class BaseManager<T> : DomainService where T : class, IEntity<Guid>
{
    protected readonly IBaseRepository<T> Repository;
    protected readonly ICustomerRepository CustomerRepository;
    protected readonly IIdentityUserRepository UserRepository;
    
    public BaseManager(
        IBaseRepository<T> repository,
        ICustomerRepository customerRepository,
        IIdentityUserRepository userRepository)
    {
        Repository = repository;
        CustomerRepository = customerRepository;
        UserRepository = userRepository;
    }

    #region Core Validations
    
    // UserId ve CustomerId'nin geçerli ve birbirine ait olduğunu kontrol eder
    public async Task<bool> ValidateUserCustomerRelationAsync(Guid userId, Guid customerId)
    {
        // 1. User var mı kontrol et
        var user = await UserRepository.GetAsync(userId, includeDetails: false);
        if (user == null)
            throw new BusinessException(BankAppDomainErrorCodes.Users.NotFound);
        // Customer var mı kontrol et
        var customer = await CustomerRepository.GetAsync(customerId);
        if (customer == null)
            throw new BusinessException(BankAppDomainErrorCodes.Customers.NotFound);
        if (customer.UserId != userId)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation);
        return true;
    }
    // Sadece User kontrolü
    public async Task<bool> ValidateUserExistsAsync(Guid userId)
    {
        var user = await UserRepository.GetAsync(userId, includeDetails: false);
        if (user == null)
            throw new BusinessException(BankAppDomainErrorCodes.Users.NotFound);
        return true;
    }
    // Sadece Customer kontrolü
    public async Task<bool> ValidateCustomerExistsAsync(Guid customerId)
    {
        var customer = await CustomerRepository.GetAsync(customerId);
        if (customer == null)
            throw new BusinessException(BankAppDomainErrorCodes.Customers.NotFound);
        return true;
    }
    #endregion
}