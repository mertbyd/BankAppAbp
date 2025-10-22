// MyAbpProjectNet7.HttpApi/Controllers/CardController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using BankApp.Dtos.Card;
using BankApp.Interface;
using BankApp.Permissions;

namespace BankApp.Controllers;

[Route("api/cards")]
[Authorize]
[ApiExplorerSettings(GroupName = "Cards")]
[Tags("Cards")] 
public class CardController :BankAppController
{
    private readonly ICardAppService _cardAppService;
    
    public CardController(ICardAppService cardAppService)
    {
        _cardAppService = cardAppService;
    }
    
    [HttpPost]
    [Authorize(BankAppPermissions.Card.Write)]
    public async Task<CardDto> Create([FromBody] CreateCardDto input)
    {
        return await _cardAppService.CreateAsync(input);
    }
    
    [HttpPut("{id}")]
    [Authorize(BankAppPermissions.Card.Write)]
    public async Task<CardDto> Update(Guid id, [FromBody] UpdateCardDto input)
    {
        return await _cardAppService.UpdateAsync(id, input);
    }
    
    [HttpGet("{id}")]
    [Authorize(BankAppPermissions.Card.Read)]
    public async Task<CardDto> Get(Guid id)
    {
        return await _cardAppService.GetAsync(id);
    }
    
    [HttpGet]
    [Authorize(BankAppPermissions.Card.Read)]
    public async Task<List<CardDto>> GetList()
    {
        return await _cardAppService.GetListAsync();
    }
    
    [HttpGet("my-cards")]
    [Authorize(BankAppPermissions.Card.Read)]
    public async Task<List<CardDto>> GetMyCards()
    {
        return await _cardAppService.GetMyCardsAsync();
    }
    
    [HttpDelete("{id}")]
    [Authorize(BankAppPermissions.Card.Delete)]
    public async Task Delete(Guid id)
    {
        await _cardAppService.DeleteAsync(id);
    }
    // HARCAMA ENDPOINTLERİ
    [HttpPost("{id}/purchase")]
    [Authorize(BankAppPermissions.Card.Purchase)]
    public async Task<CardTransactionResponseDto> MakePurchase(Guid id, [FromBody] CardTransactionDto input)
    {
        return await _cardAppService.MakePurchaseAsync(id, input);
    }
    [HttpPost("{id}/withdraw")]
    [Authorize(BankAppPermissions.Card.Withdraw)]
    public async Task<CardTransactionResponseDto> Withdraw(Guid id, [FromBody] CardTransactionDto input)
    {
        input.TransactionType =BankApp.Enums.TransactionTypes.Withdraw;
        return await _cardAppService.ProcessBankCardTransactionAsync(id, input);
    }
    [HttpPost("{id}/deposit")]
    [Authorize(BankAppPermissions.Card.Deposit)]
    public async Task<CardTransactionResponseDto> Deposit(Guid id, [FromBody] CardTransactionDto input)
    {
        input.TransactionType = BankApp.Enums.TransactionTypes.Deposit;
        return await _cardAppService.ProcessBankCardTransactionAsync(id, input);
    }
    [HttpPost("{id}/pay-credit-debt")]
    [Authorize(BankAppPermissions.Card.Write)]
    public async Task<CreditCardPaymentResponseDto> PayCreditCardDebt(Guid id, [FromBody] CreditCardPaymentDto input)
    {
        return await _cardAppService.PayCreditCardDebtAsync(id, input);
    }
}