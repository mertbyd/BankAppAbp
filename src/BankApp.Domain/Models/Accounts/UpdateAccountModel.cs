using System;
using BankApp.Enums;
namespace BankApp.Models.Accounts;

public class UpdateAccountModel
{
    public string AccountName { get; set; }
    public decimal Balance { get; set; }
    public DateTime? ClosedAt { get; set; }
}