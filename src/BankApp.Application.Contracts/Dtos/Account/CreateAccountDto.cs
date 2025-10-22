using System;
namespace BankApp.Dtos.Accounts;

public class CreateAccountDto
{
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string IBAN { get; set; }
    public Guid CustomerId { get; set; }
    public Guid UserId {get;set;}
    public decimal InitialBalance { get; set; } = 0;
    public DateTime? OpenedAt { get; set; } // Null ise DateTime.Now kullanılacak
}