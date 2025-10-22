using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using BankApp.Dtos.Transaction;
using BankApp.Interface;
using BankApp.Models.Transaction;
using BankApp.Managers;
using Volo.Abp;
using Volo.Abp.Uow;

namespace BankApp.Services;

[RemoteService(IsEnabled = false)]
public class TransactionAppService : BankAppAppService, ITransactionAppService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly TransactionManager _transactionManager;
    private readonly ILogger<TransactionAppService> _logger;
    private readonly IMapper _mapper;
    
    public TransactionAppService(
        ITransactionRepository transactionRepository,
        TransactionManager transactionManager,
        ILogger<TransactionAppService> logger,
        IMapper mapper,
        ICustomerRepository customerRepository)
        : base(customerRepository)
    {
        _transactionRepository = transactionRepository;
        _transactionManager = transactionManager;
        _logger = logger;
        _mapper = mapper;
    }
    // İşlem yaratır
    [UnitOfWork]
    public async Task<TransactionDto> CreateAsync(CreateTransactionDto input)
    {
        var model = _mapper.Map<CreateTransactionModel>(input);
        model.UserId = input.UserId;
        var transaction = await _transactionManager.CreateAsync(model);
        var createdTransaction = await _transactionRepository.InsertAsync(transaction, autoSave: true);
        var result = _mapper.Map<TransactionDto>(createdTransaction);
        return result;
    }
    // ID"ye göre işlem getir
    public async Task<TransactionDto> GetAsync(Guid id)
    {
        var transaction = await _transactionRepository.GetAsync(id);
        var result = _mapper.Map<TransactionDto>(transaction);
        return result;
    }
    // İşlemleri liste halinde getir
    public async Task<List<TransactionDto>> GetListAsync()
    {
        var transactions = await _transactionRepository.GetListAsync();
        var result = _mapper.Map<List<TransactionDto>>(transactions);
        return result;
    }
    // UserId ile işlemleri getir
    public async Task<List<TransactionDto>> GetTransactionsByUserIdAsync(Guid userId)
    {
        var transactions = await _transactionRepository.GetByUserIdAsync(userId);
        var result = _mapper.Map<List<TransactionDto>>(transactions);
        return result;
    }
    // Current user"ın işlemlerini getir
    public async Task<List<TransactionDto>> GetMyTransactionsAsync()
    {
        var userId = GetCurrentUserId();
        var transactions = await _transactionRepository.GetByUserIdAsync(userId);
        var result = _mapper.Map<List<TransactionDto>>(transactions);
        return result;
    }
}