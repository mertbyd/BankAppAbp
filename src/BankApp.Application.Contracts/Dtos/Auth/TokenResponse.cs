using System;
namespace BankApp.Dtos.Auth;

public class TokenResponse
{
    public string AccessToken { get; set; }
    public string TokenType { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
    
    // Yeni eklenen property'ler
    public Guid? UserId { get; set; }
}