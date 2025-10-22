using System;
using BankApp.Enums;
namespace BankApp.Models.Cards;

public class CreateCardModel
{
    public CardType CardType { get; set; }
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public Guid UserId { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string CVV { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? AccountId { get; set; } // Debit card için gerekli, Credit card için opsiyonel
    public decimal? CreditLimit { get; set; } // Credit card için gerekli
    public CardStatuses Status { get; set; } = CardStatuses.Active;
}