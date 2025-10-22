// BankApp.Domain/Managers/TransactionManager.cs
using BankApp.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Services;
using Microsoft.Extensions.Logging;
using BankApp.Models.Transaction;
using System.Threading.Tasks;
using BankApp.Entities;
using BankApp.Enums;
using Volo.Abp;
using BankApp.Managers.EnumManagers;
using Volo.Abp.Identity;
using System.Linq;
namespace BankApp.Managers;
public class TransactionManager : BaseManager<Transaction>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly EnumManager _enumManager;
    public TransactionManager(
        ITransactionRepository transactionRepository,
        ICustomerRepository customerRepository,
        IIdentityUserRepository userRepository,
        ICardRepository cardRepository,
        IAccountRepository accountRepository,
        IMapper mapper,
        ILogger<TransactionManager> logger, 
        EnumManager enumManager)
        : base(transactionRepository, customerRepository, userRepository)
    {
        _transactionRepository = transactionRepository;
        _cardRepository = cardRepository;
        _accountRepository = accountRepository;
        _mapper = mapper;
        _enumManager = enumManager;
    }
    public async Task<Transaction> CreateAsync(CreateTransactionModel model)
    {
        // User ve Customer ilişkisini kontrol et
        await ValidateUserCustomerRelationAsync(model.UserId, model.CustomerId);
        // Enum validasyonu (Hala domain kuralı olarak kalabilir, ancak mesajı temizlenir)
        if (!await _enumManager.IsTransactionTypeAllowedAsync(model.TransactionTypesId))
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation); 
        // Business rules validasyonu
        await ValidateBusinessRulesAsync(model);
        // Entity oluştur
        var transaction = _mapper.Map<Transaction>(model);
        transaction.SetId(GuidGenerator.Create());
        transaction.UserId = model.UserId;
        transaction.CustomerId = model.CustomerId;
        if (!model.TransactionDate.HasValue)
            transaction.TransactionDate = DateTime.Now;
        return transaction;
    }
    #region Model Validations (REMOVED)
    #endregion
    #region Business Rules
    private async Task ValidateCardExistsAndActiveAsync(Guid cardId, Guid userId)
    {
        var card = await _cardRepository.GetAsync(cardId);
        if (card == null)
            throw new BusinessException(BankAppDomainErrorCodes.Cards.NotFound); 
        // UserId kontrolü - kart kullanıcıya ait olmalı
        if (card.UserId != userId)
            throw new BusinessException(BankAppDomainErrorCodes.Cards.UnauthorizedOwnership); 
        if (card.Status != CardStatuses.Active)
            throw new BusinessException(BankAppDomainErrorCodes.Cards.BlockedCard); 
    }
    private async Task ValidateAccountExistsAndOpenAsync(Guid accountId, Guid userId)
    {
        var account = await _accountRepository.GetAsync(accountId);
        if (account == null)
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.NotFound); 
        if (account.UserId != userId)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation); 
        if (account.ClosedAt.HasValue)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation); 
    }
    private async Task ValidateBusinessRulesAsync(CreateTransactionModel model)
    {
        switch (model.TransactionTypesId)
        {
            case TransactionTypes.Transfer:
                await ValidateAccountExistsAndOpenAsync(model.SourceAccountId.Value, model.UserId);
                await ValidateAccountExistsAndOpenAsync(model.TargetAccountId.Value, model.UserId);
                if (model.SourceAccountId == model.TargetAccountId)
                    throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation); 
                // Bakiye kontrolü
                var sourceAccountForTransfer = await _accountRepository.GetAsync(model.SourceAccountId.Value);
                if (sourceAccountForTransfer.Balance < model.Amount)
                    throw new BusinessException(BankAppDomainErrorCodes.Accounts.InsufficientBalance); 
                break;
            case TransactionTypes.Deposit:
                await ValidateAccountExistsAndOpenAsync(model.TargetAccountId.Value, model.UserId);
                break;
            case TransactionTypes.Withdraw:
                await ValidateAccountExistsAndOpenAsync(model.SourceAccountId.Value, model.UserId);
                // Para çekme için bakiye kontrolü
                var sourceAccount = await _accountRepository.GetAsync(model.SourceAccountId.Value);
                if (sourceAccount.Balance < model.Amount)
                    throw new BusinessException(BankAppDomainErrorCodes.Accounts.InsufficientBalance); 
                break;
            case TransactionTypes.Purchase:
            case TransactionTypes.Payment:
                await ValidateCardExistsAndActiveAsync(model.CardId.Value, model.UserId);
                // Purchase için kredi kartı limit kontrolü
                if (model.TransactionTypesId == TransactionTypes.Purchase)
                {
                    var card = await _cardRepository.GetAsync(model.CardId.Value);
                    if (card.CardType == CardType.Credit)
                    {
                        if (!card.AvailableLimit.HasValue || card.AvailableLimit < model.Amount)
                            throw new BusinessException(BankAppDomainErrorCodes.Cards.InsufficientLimit); 
                    }
                }
                break;
        }
    }
    #endregion
    #region Helper Methods
    // Kullanıcının transaction'larını getir
    public async Task<List<Transaction>> GetUserTransactionsAsync(Guid userId)
    {
        // User kontrolü
        await ValidateUserExistsAsync(userId);
        var transactions = await _transactionRepository.GetByUserIdAsync(userId);
        return transactions;
    }
    // Transaction ownership kontrolü
    public async Task<Transaction> ValidateTransactionOwnershipAsync(Guid userId, Guid transactionId)
    {
        var transaction = await _transactionRepository.GetAsync(transactionId);
        if (transaction == null)
            throw new BusinessException(BankAppDomainErrorCodes.Transactions.NotFound); 
        if (transaction.UserId != userId)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation);
        return transaction;
    }
    #endregion
}