using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using BankApp.Dtos.Accounts;
using BankApp.Interface;
using BankApp.Interface;
using BankApp.Models.Accounts;
using BankApp.Managers;
using Volo.Abp;
using Volo.Abp.Uow;

namespace BankApp.Services;

[RemoteService(IsEnabled = false)]
public class AccountAppService : BankAppAppService,IAccountAppService
{
    private readonly IAccountRepository _accountRepository;
    private readonly AccountManager _accountManager;
    private readonly ILogger<AccountAppService> _logger;
    private readonly IMapper _mapper;
    
    public AccountAppService(
        IAccountRepository accountRepository,
        AccountManager accountManager,
        ILogger<AccountAppService> logger,
        IMapper mapper,
        ICustomerRepository customerRepository)
        : base(customerRepository)
    {
        _accountRepository = accountRepository;
        _accountManager = accountManager;
        _logger = logger;
        _mapper = mapper;
    }
    
    // Yeni hesap oluşturur
    [UnitOfWork]
    public async Task<AccountDto> CreateAsync(CreateAccountDto input)
    {
        var model = _mapper.Map<CreateAccountModel>(input);
        model.UserId = input.UserId;
        var account = await _accountManager.CreateAsync(model);
        var createdAccount = await _accountRepository.InsertAsync(account, autoSave: true);
        var result = _mapper.Map<AccountDto>(createdAccount);
        return result;
    }
    // Hesap bilgilerini günceller
    [UnitOfWork]
    public async Task<AccountDto> UpdateAsync(Guid id, UpdateAccountDto input)
    {
        var existingAccount = await _accountRepository.GetAsync(id);
        if (existingAccount == null)
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.NotFound);
        var model = _mapper.Map<UpdateAccountModel>(input);
        // Manager'a UserId ve AccountId gönder
        var updatedAccount = await _accountManager.UpdateAsync(
            existingAccount.UserId, 
            id, 
            model
        );
        var savedAccount = await _accountRepository.UpdateAsync(updatedAccount, autoSave: true);
        var result = _mapper.Map<AccountDto>(savedAccount);
        return result;
    }
    public async Task<AccountDto> GetAsync(Guid id)
    {
        var account = await _accountRepository.GetAsync(id);
        if (account == null)
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.NotFound);
        var result = _mapper.Map<AccountDto>(account);
        return result;
    }
    // Tüm hesapları listele
    public async Task<List<AccountDto>> GetListAsync()
    {
        var accounts = await _accountRepository.GetListAsync();
        var result = _mapper.Map<List<AccountDto>>(accounts);
        return result;
    }
    // UserId ile hesapları getir
    public async Task<List<AccountDto>> GetAccountsByUserIdAsync(Guid userId)
    {
        var accounts = await _accountRepository.GetByUserIdAsync(userId);
        var result = _mapper.Map<List<AccountDto>>(accounts);
        return result;
    }
    // Current user'ın hesaplarını getir
    public async Task<List<AccountDto>> GetMyAccountsAsync()
    {
        var userId = GetCurrentUserId();
        var accounts = await _accountRepository.GetByUserIdAsync(userId);
        var result = _mapper.Map<List<AccountDto>>(accounts);
        return result;
    }
    // Hesap sil (soft delete)
    [UnitOfWork]
    public async Task DeleteAsync(Guid id)
    {
        var account = await _accountRepository.GetAsync(id);
        if (account == null)
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.NotFound);
        await _accountRepository.DeleteAsync(account, autoSave: true);
    }
    // Current user'ın hesabını kapat
    [UnitOfWork]
    public async Task CloseMyAccountAsync(Guid id)
    {
        var userId = GetCurrentUserId();
        var account = await _accountRepository.GetAsync(id);
        if (account == null)
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.NotFound);
        if (account.UserId != userId)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation);
        // Manager'da business logic
        await _accountManager.CloseAccountAsync(userId, id);
        await _accountRepository.UpdateAsync(account, autoSave: true);
      
    }
}