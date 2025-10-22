using System;
using BankApp.Enums;
namespace BankApp.Models.Accounts;

public class CreateAccountModel
{
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string IBAN { get; set; }
    public Guid UserId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal InitialBalance { get; set; }
    public DateTime? OpenedAt { get; set; }
}