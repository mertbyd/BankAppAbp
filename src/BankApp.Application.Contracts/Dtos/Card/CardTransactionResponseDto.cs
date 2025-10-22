using System;
namespace BankApp.Dtos.Card;
public class CardTransactionResponseDto
{
    public Guid TransactionId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal? RemainingLimit { get; set; } // Kredi kartı için
    public decimal? AccountBalance { get; set; } // Banka kartı için (opsiyonel)
}