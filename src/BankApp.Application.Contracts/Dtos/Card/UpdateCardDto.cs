
using BankApp.Enums;
namespace BankApp.Dtos.Card;

public class UpdateCardDto
{
    public string CardHolderName { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string CVV { get; set; }
    public CardStatuses Status { get; set; }
    public decimal? CreditLimit { get; set; } // Credit card için
    public decimal UsedLimit { get; set; } // Manuel güncelleme için
}