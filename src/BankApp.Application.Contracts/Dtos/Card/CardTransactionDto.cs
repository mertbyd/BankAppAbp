using BankApp.Enums;
namespace BankApp.Dtos.Card;

public class CardTransactionDto
{
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public TransactionTypes TransactionType { get; set; } // Purchase, Deposit, Withdraw
}