using System;
using BankApp.Enums;
namespace BankApp.Models.Transaction;

public class CreateTransactionModel
{
    public TransactionTypes TransactionTypesId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? CardId { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime? TransactionDate { get; set; }
    public Guid? TargetAccountId { get; set; }
    public Guid? SourceAccountId { get; set; }
}