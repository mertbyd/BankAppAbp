using System;
using BankApp.Enums;
namespace BankApp.Dtos.Transaction;
public class CreateTransactionDto
{
    public TransactionTypes TransactionTypesId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? CardId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public Guid UserId { get; set; }
    public Guid? TargetAccountId { get; set; }
    public Guid? SourceAccountId { get; set; }
    public DateTime? TransactionDate { get; set; } // Null ise DateTime.Now kullanılacak
}