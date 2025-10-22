using System;
using BankApp.Enums;
namespace BankApp.Models.Customers;

public class UpdateCustomerModel
{
    public string FullName { get; set; }
    public string BirthPlace { get; set; }
    public Guid UserId { get; set; }
    public DateTime? BirthDate { get; set; }
    public decimal RiskLimit { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}