using System;
using BankApp.Enums;
namespace BankApp.Models.Cards;

public class UpdateCardModel
{
    public string CardHolderName { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public Guid UserId { get; set; }
    public string CVV { get; set; }
    public CardStatuses Status { get; set; }
    public decimal? CreditLimit { get; set; }
    public decimal UsedLimit { get; set; }
}