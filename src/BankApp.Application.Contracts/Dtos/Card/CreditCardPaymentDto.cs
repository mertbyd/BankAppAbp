using System;
namespace BankApp.Dtos.Card;

public class CreditCardPaymentDto
{
    public Guid SourceCardId { get; set; } // Banka kartı ID
    public decimal Amount { get; set; }
    public string Description { get; set; }
}