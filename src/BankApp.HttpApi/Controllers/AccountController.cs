// MyAbpProjectNet7.HttpApi/Controllers/AccountController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using BankApp.Dtos.Accounts;
using BankApp.Interface;
using BankApp.Permissions;

namespace BankApp.Controllers ;

[Route("api/accounts")]
[Authorize]
[ApiExplorerSettings(GroupName = "Accounts")]
[Tags("Accounts")] 

public class AccountController : BankAppController
{
    private readonly IAccountAppService _accountAppService;

    public AccountController(IAccountAppService accountAppService)
    {
        _accountAppService = accountAppService;
    }
    [HttpPost]
    [Authorize(BankAppPermissions.Account.Write)]
    public async Task<AccountDto> Create([FromBody] CreateAccountDto input)
    {
        return await _accountAppService.CreateAsync(input);
    }
    [HttpPut("{id}")]
    [Authorize(BankAppPermissions.Account.Write)]
    public async Task<AccountDto> Update(Guid id, [FromBody] UpdateAccountDto input)
    {
        return await _accountAppService.UpdateAsync(id, input);
    }
    [HttpGet("{id}")]
    [Authorize(BankAppPermissions.Account.Read)]
    public async Task<AccountDto> Get(Guid id)
    {
        return await _accountAppService.GetAsync(id);
    }
    [HttpGet]
    [Authorize(BankAppPermissions.Account.Read)]
    public async Task<List<AccountDto>> GetList()
    {
        return await _accountAppService.GetListAsync();
    }
    [HttpDelete("{id}")]
    [Authorize(BankAppPermissions.Account.Delete)]
    public async Task Delete(Guid id)
    {
        await _accountAppService.DeleteAsync(id);
    }
}