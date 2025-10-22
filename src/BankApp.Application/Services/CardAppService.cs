// BankApp.Application/Services/CardAppService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using BankApp.Dtos.Card;
using BankApp.Models.Cards;
using BankApp.Models.Transaction;
using BankApp.Managers;
using BankApp.Enums;
using BankApp.Interface;
using Volo.Abp.Uow;
using Volo.Abp;

namespace BankApp.Services;

[RemoteService(IsEnabled = false)]
public class CardAppService : BankAppAppService, ICardAppService
{
    private readonly ICardRepository _cardRepository;
    private readonly CardManager _cardManager;
    private readonly ILogger<CardAppService> _logger;
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _transactionRepository;
    private readonly TransactionManager _transactionManager;
    private readonly IAccountRepository _accountRepository;
    
    public CardAppService(
        ICardRepository cardRepository,
        CardManager cardManager,
        ILogger<CardAppService> logger,
        IMapper mapper,
        ITransactionRepository transactionRepository,
        TransactionManager transactionManager,
        IAccountRepository accountRepository,
        ICustomerRepository customerRepository)
        : base(customerRepository)
    {
        _cardRepository = cardRepository;
        _cardManager = cardManager;
        _logger = logger;
        _mapper = mapper;
        _transactionRepository = transactionRepository;
        _transactionManager = transactionManager;
        _accountRepository = accountRepository;
    }
    
    // Kart ekleme
    [UnitOfWork]
    public async Task<CardDto> CreateAsync(CreateCardDto input)
    {
        // Model oluştur
        var model = _mapper.Map<CreateCardModel>(input);
        model.UserId = input.UserId;
        model.CustomerId = input.CustomerId;
        // Business logic - Manager
        var card = await _cardManager.CreateAsync(model);
        // Persistence
        var createdCard = await _cardRepository.InsertAsync(card);
        var result = _mapper.Map<CardDto>(createdCard);
        return result;
    }
    
    // Kart güncelleme
    [UnitOfWork]
    public async Task<CardDto> UpdateAsync(Guid id, UpdateCardDto input)
    {
        // Kartı getir
        var card = await _cardManager.GetByIdAsync(id);
        // Update model oluştur
        var model = _mapper.Map<UpdateCardModel>(input);
        // Manager ile güncelle
        var updatedCard = await _cardManager.UpdateAsync(id, model);
        // Veritabanına kaydet
        var savedCard = await _cardRepository.UpdateAsync(updatedCard);
        var result = _mapper.Map<CardDto>(savedCard);
        return result;
    }
    
    // ID'ye göre getirme
    public async Task<CardDto> GetAsync(Guid id)
    {
        var card = await _cardManager.GetByIdAsync(id);
        return _mapper.Map<CardDto>(card);
    }
    
    // Current user'ın kartlarını listeleme
    public async Task<List<CardDto>> GetMyCardsAsync()
    {
        var userId = GetCurrentUserId();
        var cards = await _cardRepository.GetByUserIdAsync(userId);
        var result = _mapper.Map<List<CardDto>>(cards);
        return result;
    }
    
    // Kart silme
    [UnitOfWork]
    public async Task DeleteAsync(Guid id)
    {
        var card = await _cardManager.GetByIdAsync(id);
        await _cardRepository.DeleteAsync(card);
    }
    
    // Kredi kartı ile harcama - Sadece kendi kartıyla
    [UnitOfWork]
    public async Task<CardTransactionResponseDto> MakePurchaseAsync(Guid cardId, CardTransactionDto input)
    {
        var userId = GetCurrentUserId();
        // Kart kontrolü ve sahiplik doğrulama
        var card = await _cardManager.GetByIdAsync(cardId);
        if (!await _cardManager.IsCardOwnedByUserAsync(userId, cardId))
            throw new BusinessException(BankAppDomainErrorCodes.Cards.UnauthorizedOwnership);
        // Purchase validasyonları
        var validatedCard = await _cardManager.ProcessPurchaseAsync(cardId, input.Amount);
        // Transaction oluştur
        var transactionModel = new CreateTransactionModel
        {
            TransactionTypesId = TransactionTypes.Purchase,
            UserId = userId,
            CustomerId = validatedCard.CustomerId,
            CardId = cardId,
            Description = input.Description ?? "Kredi kartı harcaması",
            Amount = input.Amount
        };
        var transaction = await _transactionManager.CreateAsync(transactionModel);
        await _transactionRepository.InsertAsync(transaction);
        // Kart güncellendi (ProcessPurchaseAsync içinde yapıldı)
        await _cardRepository.UpdateAsync(validatedCard);
        // Response
        var response = _mapper.Map<CardTransactionResponseDto>(transaction);
        response.RemainingLimit = validatedCard.AvailableLimit;
        return response;
    }
    
    // Banka kartı işlemleri - Para yatırma/çekme
    [UnitOfWork]
    public async Task<CardTransactionResponseDto> ProcessBankCardTransactionAsync(Guid cardId, CardTransactionDto input)
    {
        var userId = GetCurrentUserId();
        // Kart kontrolü ve sahiplik doğrulama
        var card = await _cardManager.GetByIdAsync(cardId);
        if (!await _cardManager.IsCardOwnedByUserAsync(userId, cardId))
            throw new BusinessException(BankAppDomainErrorCodes.Cards.UnauthorizedOwnership);
        // İşlem tipine göre validation
        var validatedResult = input.TransactionType == TransactionTypes.Withdraw
            ? await _cardManager.ProcessWithdrawAsync(cardId, input.Amount)
            : await _cardManager.ProcessDepositAsync(cardId, input.Amount);
        if (!validatedResult.card.AccountId.HasValue)
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.NotFound);
        // Transaction oluştur
        var transactionModel = new CreateTransactionModel
        {
            TransactionTypesId = input.TransactionType,
            UserId = userId,
            CustomerId = validatedResult.card.CustomerId,
            CardId = cardId,
            Description = input.Description ?? 
                (input.TransactionType == TransactionTypes.Deposit ? "Para yatırma" : "Para çekme"),
            Amount = input.Amount,
            TargetAccountId = input.TransactionType == TransactionTypes.Deposit ? validatedResult.card.AccountId : null,
            SourceAccountId = input.TransactionType == TransactionTypes.Withdraw ? validatedResult.card.AccountId : null
        };
        var transaction = await _transactionManager.CreateAsync(transactionModel);
        await _transactionRepository.InsertAsync(transaction);
        // Hesap bakiyesi güncelleme
        var account = validatedResult.account;
        if (input.TransactionType == TransactionTypes.Deposit)
        {
            account.Balance += input.Amount;
        }
        else if (input.TransactionType == TransactionTypes.Withdraw)
        {
            account.Balance -= input.Amount;
        }
        await _accountRepository.UpdateAsync(account);
        // Response
        var response = _mapper.Map<CardTransactionResponseDto>(transaction);
        response.AccountBalance = account.Balance;
        return response;
    }
    
    // Kredi kartı borcu ödeme - Kendi kartları arasında
    [UnitOfWork]
    public async Task<CreditCardPaymentResponseDto> PayCreditCardDebtAsync(Guid targetCardId, CreditCardPaymentDto input)
    {
        var userId = GetCurrentUserId();
        // Her iki kartın kontrolü
        var targetCard = await _cardManager.GetByIdAsync(targetCardId);
        if (!await _cardManager.IsCardOwnedByUserAsync(userId, targetCardId))
            throw new BusinessException(BankAppDomainErrorCodes.Cards.UnauthorizedOwnership);
        var sourceCard = await _cardManager.GetByIdAsync(input.SourceCardId);
        if (!await _cardManager.IsCardOwnedByUserAsync(userId, input.SourceCardId))
            throw new BusinessException(BankAppDomainErrorCodes.Cards.UnauthorizedOwnership);
        var paymentResult = await _cardManager.ProcessPaymentAsync(targetCardId, input.Amount);
        var withdrawResult = await _cardManager.ProcessWithdrawAsync(input.SourceCardId, input.Amount);
        if (!withdrawResult.card.AccountId.HasValue)
            throw new BusinessException(BankAppDomainErrorCodes.Accounts.NotFound);
        var withdrawTransactionModel = new CreateTransactionModel
        {
            TransactionTypesId = TransactionTypes.Withdraw,
            UserId = userId,
            CustomerId = withdrawResult.card.CustomerId,
            CardId = input.SourceCardId,
            Description = $"Kredi kartı ödemesi - {input.Description}",
            Amount = input.Amount,
            SourceAccountId = withdrawResult.card.AccountId
        };
        var withdrawTransaction = await _transactionManager.CreateAsync(withdrawTransactionModel);
        await _transactionRepository.InsertAsync(withdrawTransaction);
        var paymentTransactionModel = new CreateTransactionModel
        {
            TransactionTypesId = TransactionTypes.Payment,
            UserId = userId,
            CustomerId = paymentResult.CustomerId,
            CardId = targetCardId,
            Description = $"Borç ödemesi - {input.Description}",
            Amount = input.Amount
        };
        var paymentTransaction = await _transactionManager.CreateAsync(paymentTransactionModel);
        await _transactionRepository.InsertAsync(paymentTransaction);
        // Hesap bakiyesi güncelleme
        withdrawResult.account.Balance -= input.Amount;
        await _accountRepository.UpdateAsync(withdrawResult.account);
        // Kartları güncelle
        await _cardRepository.UpdateAsync(paymentResult);
        await _cardRepository.UpdateAsync(withdrawResult.card);
        // Response
        var response = _mapper.Map<CreditCardPaymentResponseDto>(paymentTransaction);
        response.NewAvailableLimit = paymentResult.AvailableLimit.Value;
        response.NewUsedLimit = paymentResult.UsedLimit;
        return response;
    }
    // Tüm kartları listele
    public async Task<List<CardDto>> GetListAsync()
    {
        var cards = await _cardRepository.GetListAsync();
        return _mapper.Map<List<CardDto>>(cards);
    }
}