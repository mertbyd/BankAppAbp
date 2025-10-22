using System;
using BankApp.Enums;
namespace BankApp.Models.Cards;
public class CreditCardPaymentModel
{
    public Guid SourceCardId { get; set; }
    public Guid TargetCardId { get; set; }
    public decimal Amount { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime? TransactionDate { get; set; }
}