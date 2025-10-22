using System;
using BankApp.Enums;
namespace BankApp.Models.Cards;
public class CardTransactionModel
{
    public decimal Amount { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public TransactionTypes TransactionType { get; set; }
    public Guid CardId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime? TransactionDate { get; set; }
}