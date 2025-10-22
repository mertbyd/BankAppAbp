using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankApp.Dtos.Accounts;
using Volo.Abp.Application.Services;

namespace BankApp.Interface;

public interface IAccountAppService : IApplicationService
{
    // Yeni hesap oluşturur
    Task<AccountDto> CreateAsync(CreateAccountDto input);
    
    // Hesap bilgilerini günceller
    Task<AccountDto> UpdateAsync(Guid id, UpdateAccountDto input);
    
    // ID ile hesap getirir
    Task<AccountDto> GetAsync(Guid id);
    
    // Tüm hesapları getirir
    Task<List<AccountDto>> GetListAsync();
    
    // Belirli bir User'ın hesaplarını getirir
    Task<List<AccountDto>> GetAccountsByUserIdAsync(Guid userId);
    
    // Current user'ın hesaplarını getir
    Task<List<AccountDto>> GetMyAccountsAsync();
    
    // Hesap siler
    Task DeleteAsync(Guid id);
    
    // Current user'ın hesabını kapatır
    Task CloseMyAccountAsync(Guid id);
}