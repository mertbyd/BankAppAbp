// BankApp.Domain/Managers/AccountManager.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankApp.Entities;
using BankApp.Interface;
using BankApp.Models.Accounts;
using BankApp.Managers.EnumManagers;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace BankApp.Managers;

public class AccountManager : BaseManager<Account>
{
    private readonly IAccountRepository _accountRepository;
    private readonly EnumManager _enumManager;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountManager> _logger;

    public AccountManager(
        IAccountRepository accountRepository,
        ICustomerRepository customerRepository,
        IIdentityUserRepository userRepository, 
        EnumManager enumManager,
        IMapper mapper,
        ILogger<AccountManager> logger)
        : base(accountRepository, customerRepository, userRepository) 
    {
        _accountRepository = accountRepository;
        _enumManager = enumManager;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Account> CreateAsync(CreateAccountModel model)
    {
        await ValidateUserCustomerRelationAsync(model.UserId, model.CustomerId);
        await ValidateAccountNumberUniquenessAsync(model.AccountNumber);
        await ValidateIBANUniquenessAsync(model.IBAN);
        var account = _mapper.Map<Account>(model);
        account.setid(GuidGenerator.Create());
        account.UserId = model.UserId;
        account.CustomerId = model.CustomerId; 
        account.ClosedAt = null;
        return account;
    }
    public async Task<Account> UpdateAsync(Guid currentUserId, Guid accountId, UpdateAccountModel model)
    {
        // Account'u getir
        var existingAccount = await _accountRepository.GetAsync(accountId);
        if (existingAccount == null)
        {
            throw new BusinessException(
                BankAppDomainErrorCodes.Accounts.NotFound);
        }
        if (existingAccount.UserId != currentUserId)
        {
            // Admin kontrolü yapılabilir
            throw new BusinessException(
                BankAppDomainErrorCodes.Auth.AccessDenied);
        }
        // Business rule validasyonu
        ValidateAccountNotClosed(existingAccount);
        
        // AutoMapper ile güncelleme (sadece izin verilen alanları günceller)
        _mapper.Map(model, existingAccount);
        return existingAccount;
    }
    #region Business Rules
    


    private async Task ValidateAccountNumberUniquenessAsync(string accountNumber)
    {
        var allAccounts = await _accountRepository.GetListAsync();
        var result = allAccounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
        if (result != null)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation);
    }
    private async Task ValidateIBANUniquenessAsync(string iban)
    {
        var allAccounts = await _accountRepository.GetListAsync();
        var existingAccount = allAccounts.FirstOrDefault(x => x.IBAN == iban);
        if (existingAccount != null)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation);
    }
    private void ValidateAccountNotClosed(Account account)
    {
        if (account.ClosedAt.HasValue)
            throw new BusinessException(
                BankAppDomainErrorCodes.Accounts.CanNotDelete);
    }
    #endregion
    #region Helper Methods

    public async Task<bool> IsAccountActiveAsync(Guid accountId)
    {
        var account = await _accountRepository.GetAsync(accountId);
        return account != null && !account.ClosedAt.HasValue;
    }
    public async Task CloseAccountAsync(Guid currentUserId, Guid accountId)
    {
        var account = await _accountRepository.GetAsync(accountId);
        if (account == null)
            throw new BusinessException(
                BankAppDomainErrorCodes.Accounts.NotFound);
        
        if (account.UserId != currentUserId)
            throw new BusinessException(
                BankAppDomainErrorCodes.Auth.AccessDenied);
        
        ValidateAccountCanBeClosed(account);
        account.ClosedAt = DateTime.Now;
    }
    private void ValidateAccountCanBeClosed(Account account)
    {
        if (account.ClosedAt.HasValue)
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.CanNotDelete);
        if (account.Balance > 0)
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.CanNotDelete);
        if (account.Balance < 0)
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.CanNotDelete);
    }

    #endregion
}