using System;
namespace BankApp.Dtos.Card;            

public class CreditCardPaymentResponseDto
{
    public Guid TransactionId { get; set; }
    public decimal Amount { get; set; }
    public decimal NewAvailableLimit { get; set; }
    public decimal NewUsedLimit { get; set; }
    public DateTime TransactionDate { get; set; }
}