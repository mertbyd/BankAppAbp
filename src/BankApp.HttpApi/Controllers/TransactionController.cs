// MyAbpProjectNet7.HttpApi/Controllers/TransactionController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using BankApp.Dtos.Transaction;
using BankApp.Interface;
using BankApp.Permissions;

namespace BankApp.Controllers;

[Route("api/transactions")]
[Authorize]
[Tags("Transactions")] 
public class TransactionController : BankAppController
{
    private readonly ITransactionAppService _transactionAppService;
    public TransactionController(ITransactionAppService transactionAppService)
    {
        _transactionAppService = transactionAppService;
    }
    [HttpGet("{id}")]
    [Authorize(BankAppPermissions.Transaction.Read)]
    public async Task<TransactionDto> Get(Guid id)
    {
        return await _transactionAppService.GetAsync(id);
    }
    [HttpGet]
    [Authorize(BankAppPermissions.Transaction.Read)]
    public async Task<List<TransactionDto>> GetList()
    {
        return await _transactionAppService.GetListAsync();
    }
    [HttpGet("by-transactions")]
    [Authorize(BankAppPermissions.Transaction.Read)]
    public async Task<List<TransactionDto>> GetTransactionsByCustomerId( )
    {
        return await _transactionAppService.GetMyTransactionsAsync();
    }
}