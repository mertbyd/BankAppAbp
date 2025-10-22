
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using BankApp.Dtos.Auth;
using BankApp.Models.Auth;
using BankApp.Managers;
using BankApp.Interface;
using BankApp.Models.Customers;
using Volo.Abp;
using Volo.Abp.Uow;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Linq;

namespace BankApp.Services;

[RemoteService(IsEnabled = false)] 
public class AuthAppService : ApplicationService, IAuthAppService
{
    private readonly AuthManager _authManager;
    private readonly CustomerManager _customerManager;  
    private readonly ICustomerRepository _customerRepository;
    private readonly IIdentityUserRepository _identityUserRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthAppService> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
   
    
    public AuthAppService(
        AuthManager authManager,
        CustomerManager customerManager,  
        ICustomerRepository customerRepository,  
        IIdentityUserRepository identityUserRepository,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        ILogger<AuthAppService> logger,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _authManager = authManager;
        _customerManager = customerManager;
        _customerRepository = customerRepository;
        _identityUserRepository = identityUserRepository;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    [UnitOfWork]
    public async Task<Guid> RegisterAsync(RegisterDto input)
    {
        if (input == null)
            throw new BusinessException(BankAppDomainErrorCodes.General.ValidationFailed);
        var registerModel = _mapper.Map<RegisterModel>(input);
        var user = await _authManager.CreateUserAsync(registerModel);
        return user.Id;
    }
    public async Task<TokenResponse> LoginAsync(LoginDto input)
    {
        var loginModel = _mapper.Map<LoginModel>(input);
        var user = await _authManager.ValidateLoginAsync(loginModel);
        var tokenResponse = await GetTokenFromOpenIddictAsync(input.UserName, input.Password);
        if (tokenResponse == null)
            throw new BusinessException(BankAppDomainErrorCodes.Auth.InvalidToken);
        tokenResponse.UserId = user.Id;
        return tokenResponse;
    }
    public async Task<TokenResponse> GetTokenFromOpenIddictAsync(string username, string password)
    {
        var baseUrl = _configuration["App:SelfUrl"] ?? "https://localhost:44327"; 
        var httpClient = _httpClientFactory.CreateClient();
        var tokenRequest = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password),
            new KeyValuePair<string, string>("client_id", "BankApp_App"),
            new KeyValuePair<string, string>("scope", "openid profile email roles BankApp") 
        });
        var response = await httpClient.PostAsync($"{baseUrl}/connect/token", tokenRequest);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var tokenResponseJson = await response.Content.ReadAsStringAsync();
        var tokenData = JsonSerializer.Deserialize<JsonElement>(tokenResponseJson);
        return new TokenResponse
        {
            AccessToken = tokenData.GetProperty("access_token").GetString(),
            TokenType = tokenData.GetProperty("token_type").GetString(),
            ExpiresIn = tokenData.GetProperty("expires_in").GetInt32(),
            RefreshToken = tokenData.TryGetProperty("refresh_token", out var refreshToken) 
                ? refreshToken.GetString() 
                : null
        };
    }
}