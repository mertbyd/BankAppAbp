// MyAbpProjectNet7.HttpApi/Controllers/CustomerController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using BankApp.Customers.Dtos;
using BankApp.Interface;
using BankApp.Permissions;

namespace BankApp.Controllers;

[Route("api/customers")]
[Authorize]
[ApiExplorerSettings(GroupName = "Customers")]
[Tags("Customers")]
public class CustomerController:BankAppController
{
    private readonly ICustomerAppService _customerAppService;
    public CustomerController(ICustomerAppService customerAppService)
    {
        _customerAppService = customerAppService;
    }
    [HttpPost]
    [Authorize(BankAppPermissions.Customer.Write)]
    public async Task<CustomerDto> Create([FromBody] CreateCustomerDto input)
    {
        return await _customerAppService.CreateAsync(input);
    }
    [HttpPut("{id}")]
    [Authorize(BankAppPermissions.Customer.Write)]
    public async Task<CustomerDto> Update(Guid id, [FromBody] UpdateCustomerDto input)
    {
        return await _customerAppService.UpdateAsync(id, input);
    }
    [HttpGet("{id}")]
    [Authorize(BankAppPermissions.Customer.Read)]
    public async Task<CustomerDto> Get(Guid id)
    {
        return await _customerAppService.GetAsync(id);
    }
    [HttpGet]
    [Authorize(BankAppPermissions.Customer.ViewAll)]
    public async Task<List<CustomerDto>> GetList()
    {
        return await _customerAppService.GetListAsync();
    }
    [HttpDelete("{id}")]
    [Authorize(BankAppPermissions.Customer.Delete)]
    public async Task Delete(Guid id)
    {
        await _customerAppService.DeleteAsync(id);
    }
}