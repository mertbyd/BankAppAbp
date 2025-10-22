using System;
using BankApp.Enums;
namespace BankApp.Dtos.Card;

public class CardDto
{
    public Guid Id { get; set; }
    public CardType CardType { get; set; }
    public string CardNumber { get; set; }
    public Guid UserId { get; set; }
    public string CardHolderName { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string CVV { get; set; }
    public CardStatuses Status { get; set; }
    public decimal? CreditLimit { get; set; }
    public decimal? AvailableLimit { get; set; }
    public decimal UsedLimit { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? AccountId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}