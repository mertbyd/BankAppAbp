using System;
namespace BankApp.Dtos.Accounts;

public class UpdateAccountDto
{
    public string AccountName { get; set; }
    public decimal Balance { get; set; }
    public DateTime? ClosedAt { get; set; } // Hesap kapatma tarihi
}