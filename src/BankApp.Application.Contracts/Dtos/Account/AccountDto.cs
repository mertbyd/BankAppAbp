using System;
namespace BankApp.Dtos.Accounts;

public class AccountDto
{
    public Guid Id { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public DateTime OpenedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public Guid CustomerId { get; set; }
}