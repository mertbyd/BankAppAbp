using System;
namespace BankApp.Dtos.Auth;

public class LoginResponseDto
{
    public bool Success { get; set; }
    public string AccessToken { get; set; }
    public string TokenType { get; set; } = "Bearer";
    public int ExpiresIn { get; set; } // Saniye cinsinden
    public DateTime ExpiresAt { get; set; } // Token'ın tam bitiş zamanı
    public Guid UserId { get; set; }
    public Guid CustomerId { get; set; }

}