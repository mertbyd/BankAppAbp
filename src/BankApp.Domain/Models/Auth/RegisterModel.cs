using System;
namespace BankApp.Models.Auth;

public class RegisterModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string TcNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? RoleName { get; set; } 
    public string? BirthPlace { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Address { get; set; }
}