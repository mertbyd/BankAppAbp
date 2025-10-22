using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankApp.Dtos.Card;
using Volo.Abp.Application.Services;

namespace BankApp.Interface;

public interface ICardAppService : IApplicationService
{
    // Kart oluşturma
    Task<CardDto> CreateAsync(CreateCardDto input);
    
    // Kart güncelleme
    Task<CardDto> UpdateAsync(Guid id, UpdateCardDto input);
    
    // ID ile kart getirme
    Task<CardDto> GetAsync(Guid id);
    
    // Tüm kartları listeleme
    Task<List<CardDto>> GetListAsync();
    
    // Current user"ın kartlarını listeleme
    Task<List<CardDto>> GetMyCardsAsync();
    
    // Kart silme
    Task DeleteAsync(Guid id);
    
    // Kredi kartı ile harcama yapma
    Task<CardTransactionResponseDto> MakePurchaseAsync(Guid cardId, CardTransactionDto input);
    
    // Banka kartı işlemleri (Para yatırma/çekme)
    Task<CardTransactionResponseDto> ProcessBankCardTransactionAsync(Guid cardId, CardTransactionDto input);
    
    // Kredi kartı borcu ödeme
    Task<CreditCardPaymentResponseDto> PayCreditCardDebtAsync(Guid targetCardId, CreditCardPaymentDto input);
}