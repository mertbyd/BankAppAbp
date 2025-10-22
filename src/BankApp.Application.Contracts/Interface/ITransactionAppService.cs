using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankApp.Dtos.Transaction;
using Volo.Abp.Application.Services;

namespace BankApp.Interface;

public interface ITransactionAppService : IApplicationService
{
    // İşlem oluştur
    Task<TransactionDto> CreateAsync(CreateTransactionDto input);
    // ID ile işlem getir
    Task<TransactionDto> GetAsync(Guid id);
    // Tüm işlemleri listele
    Task<List<TransactionDto>> GetListAsync();
    // UserId ile işlemleri getir
    Task<List<TransactionDto>> GetTransactionsByUserIdAsync(Guid userId);
    // Current user"ın işlemlerini getir
    Task<List<TransactionDto>> GetMyTransactionsAsync();
}