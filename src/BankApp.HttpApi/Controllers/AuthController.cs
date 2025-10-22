// MyAbpProjectNet7.HttpApi/Controllers/AuthController.cs
using System;
using System.Threading.Tasks;
using BankApp;
using Microsoft.AspNetCore.Mvc;
using BankApp.Dtos.Auth;
using BankApp.Interface;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp.OpenIddict.Tokens;

namespace BankApp.Controllers;

[Route("api/auth")]
[ApiController]

public class AuthController:BankAppController
{
    private readonly IAuthAppService _authAppService;
    public AuthController(IAuthAppService authAppService)
    {
        _authAppService = authAppService;
    }
    [HttpPost("login")] //
    public async Task<TokenResponse> Login([FromBody] LoginDto input)
    {
        var token =await _authAppService.LoginAsync(input);
        return token;
    }
    [HttpPost("register")]
    public async Task<string> Register([FromBody] RegisterDto input)
    {
        var userId = await _authAppService.RegisterAsync(input);
        return userId.ToString();
    }
}