// BankApp.Domain/Managers/CardManager.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankApp.Entities;
using BankApp.Enums;
using BankApp.Interface;
using BankApp.Managers.EnumManagers;
using BankApp.Models.Cards;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Identity;

namespace BankApp.Managers;

public class CardManager : BaseManager<Card>
{
    private readonly ICardRepository _cardRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CardManager> _logger;
    private readonly EnumManager _enumManager;

    public CardManager(
        ICardRepository cardRepository,
        ICustomerRepository customerRepository,
        IIdentityUserRepository userRepository,
        IAccountRepository accountRepository,
        IMapper mapper,
        ILogger<CardManager> logger,
        EnumManager enumManager)
        : base(cardRepository, customerRepository, userRepository)
    {
        _cardRepository = cardRepository;
        _accountRepository = accountRepository;
        _mapper = mapper;
        _logger = logger;
        _enumManager = enumManager;
    }

    public async Task<Card> CreateAsync(CreateCardModel model)
    {
        await ValidateUserCustomerRelationAsync(model.UserId, model.CustomerId);
        if (!await _enumManager.IsCardTypeAllowedAsync(model.CardType))
            throw new BusinessException(BankAppDomainErrorCodes.Cards.InvalidCardType)
                .WithData("CardType", model.CardType);
        await ValidateCardNumberUniquenessAsync(model.CardNumber);
        // Debit card için account kontrolü (Business Rule)
        if (model.CardType == CardType.Debit)
        {
          
            if (!model.AccountId.HasValue)
                throw new BusinessException(BankAppDomainErrorCodes.Accounts.NotFound);
            await ValidateAccountBelongsToUserAsync(model.UserId, model.AccountId.Value);
        }
        // Entity oluştur
        var card = _mapper.Map<Card>(model);
        card.SetId(GuidGenerator.Create());
        card.UserId = model.UserId;
        card.CustomerId = model.CustomerId;
        // Credit card için limit ayarla
        if (model.CardType == CardType.Credit)
        {
            // FluentValidation'da CreditLimit > 0 kontrolü yapılıyor.
            card.AvailableLimit = model.CreditLimit;
            card.UsedLimit = 0;
        }
        return card;
    }
    public async Task<Card> UpdateAsync(Guid cardId, UpdateCardModel model)
    {
        // Card'ı getir
        var existingCard = await _cardRepository.GetAsync(cardId);
        if (existingCard == null)
            throw new BusinessException(BankAppDomainErrorCodes.Cards.NotFound);
        _mapper.Map(model, existingCard);
        // Credit card limit güncelleme (Business Rule)
        if (existingCard.CardType == CardType.Credit && model.CreditLimit.HasValue)
        {
            var newLimit = model.CreditLimit.Value;
            if (newLimit < existingCard.UsedLimit)
                throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation);
            existingCard.CreditLimit = newLimit;
            existingCard.AvailableLimit = newLimit - existingCard.UsedLimit;
        }
        return existingCard;
    }
    public async Task<Card> GetByIdAsync(Guid cardId)
    {
        var card = await _cardRepository.GetAsync(cardId);
        if (card == null)
            throw new BusinessException(BankAppDomainErrorCodes.Cards.NotFound);
        return card;
    }
    public async Task<bool> IsCardOwnedByUserAsync(Guid userId, Guid cardId)
    {
        var card = await _cardRepository.GetAsync(cardId);
        return card != null && card.UserId == userId;
    }
    #region Business Operation Methods
    public async Task<Card> ProcessPurchaseAsync(Guid cardId, decimal amount)
    {
        var card = await GetByIdAsync(cardId);
        ValidateCardStatus(card);
        ValidateTransactionAmount(amount);
        ValidateCreditCard(card);
        ValidateCreditLimit(card, amount);
        card.UsedLimit += amount;
        card.AvailableLimit = card.CreditLimit - card.UsedLimit;
        return card;
    }
    public async Task<Card> ProcessPaymentAsync(Guid cardId, decimal amount)
    {
        var card = await GetByIdAsync(cardId);
        ValidateCardStatus(card);
        ValidateTransactionAmount(amount);
        ValidateCreditCard(card);
        if (amount > card.UsedLimit) 
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation);
        card.UsedLimit -= amount;
        card.AvailableLimit = card.CreditLimit - card.UsedLimit;
        return card;
    }

    public async Task<(Card card, Account account)> ProcessWithdrawAsync(Guid cardId, decimal amount)
    {
        var card = await GetByIdAsync(cardId);
        // İşlem validasyonları (Bunlar Business Rule olduğu için kalır)
        ValidateCardStatus(card);
        // Transaction Amount validation artık DTO'da yapılıyor. Burada sadece 0 kontrolü kalır.
        ValidateTransactionAmount(amount);
        ValidateDebitCard(card);
        var account = await ValidateAndGetAccountAsync(card.AccountId.Value, amount); // Business Rule
        return (card, account);
    }

    public async Task<(Card card, Account account)> ProcessDepositAsync(Guid cardId, decimal amount)
    {
        var card = await GetByIdAsync(cardId);
        ValidateCardStatus(card);
        ValidateTransactionAmount(amount);
        ValidateDebitCard(card);
        var account = await _accountRepository.GetAsync(card.AccountId.Value);
        if (account == null) // Business Rule
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.NotFound);
        return (card, account);
    }
    #endregion
    #region Model Validations (Removed/Cleaned)
    #endregion
    #region Helper Methods (Business Rules)
    private async Task ValidateAccountBelongsToUserAsync(Guid userId, Guid accountId)
    {
        var account = await _accountRepository.GetAsync(accountId);
        if (account == null)
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.NotFound);
        if (account.UserId != userId)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation);
    }
    private async Task ValidateCardNumberUniquenessAsync(string cardNumber)
    {
        var cards = await _cardRepository.GetListAsync();
        var existingCard = cards.FirstOrDefault(x => x.CardNumber == cardNumber);
        if (existingCard != null)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation);
    }
    private void ValidateCardStatus(Card card)
    {
        if (card.Status != CardStatuses.Active)
            throw new BusinessException(BankAppDomainErrorCodes.Cards.BlockedCard);
    }
    private void ValidateCreditCard(Card card)
    {
        if (card.CardType != CardType.Credit)
            throw new BusinessException(BankAppDomainErrorCodes.Cards.InvalidCardType);
    }
    private void ValidateDebitCard(Card card)
    {
        if (card.CardType != CardType.Debit)
            throw new BusinessException(BankAppDomainErrorCodes.Cards.InvalidCardType);
        if (!card.AccountId.HasValue)
            throw new BusinessException(BankAppDomainErrorCodes.Cards.InvalidCardType);
    }
    
    private void ValidateCreditLimit(Card card, decimal amount)
    {
        if (!card.AvailableLimit.HasValue)
            throw new BusinessException(BankAppDomainErrorCodes.Cards.InvalidCardType);
        if (card.AvailableLimit < amount)
            throw new BusinessException(BankAppDomainErrorCodes.Cards.InsufficientLimit);
    }
    private void ValidateTransactionAmount(decimal amount)
    {
        if (amount <= 0)
            throw new BusinessException(BankAppDomainErrorCodes.Transactions.InvalidAmount);
    }
    
    private async Task<Account> ValidateAndGetAccountAsync(Guid accountId, decimal amount)
    {
        var account = await _accountRepository.GetAsync(accountId);
        if (account == null)
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.NotFound);
        if (account.ClosedAt.HasValue)
            throw new BusinessException(BankAppDomainErrorCodes.General.InvalidOperation);
        if (account.Balance < amount)
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.InsufficientBalance);
        return account;
    }
    #endregion
}