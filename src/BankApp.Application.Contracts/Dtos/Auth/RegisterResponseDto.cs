using System;
namespace BankApp.Dtos.Auth;

public class RegisterResponseDto
{
    public bool Success { get; set; }
    public Guid UserId { get; set; }
    public Guid CustomerId { get; set; }
    public string Message { get; set; }
}