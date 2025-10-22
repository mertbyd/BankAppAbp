using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using BankApp.Dtos.Auth;

namespace BankApp.Interface;

public interface IAuthAppService : IApplicationService
{
    Task<Guid> RegisterAsync(RegisterDto input);
    Task<TokenResponse> LoginAsync(LoginDto input);
    Task<TokenResponse> GetTokenFromOpenIddictAsync(string username, string password);
}