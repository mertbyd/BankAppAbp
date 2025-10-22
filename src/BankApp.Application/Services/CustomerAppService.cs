using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Volo.Abp;
using Volo.Abp.Uow;
using BankApp.Interface;
using BankApp.Managers;
using BankApp.Customers.Dtos;
using BankApp.Models.Customers;

namespace BankApp.Services;

[RemoteService(IsEnabled = false)]
public class CustomerAppService : BankAppAppService, ICustomerAppService  
{
    private readonly ICustomerRepository _customerRepository;
    private readonly CustomerManager _customerManager;
    private readonly ILogger<CustomerAppService> _logger;
    private readonly IMapper _mapper;
    public CustomerAppService(
        ICustomerRepository customerRepository,
        CustomerManager customerManager,
        ILogger<CustomerAppService> logger,
        IMapper mapper)  
        : base(customerRepository)
    {
        _customerRepository = customerRepository;
        _customerManager = customerManager;
        _logger = logger;
        _mapper = mapper;
    }
    // Tüm müşterileri getirir
    public async Task<List<CustomerDto>> GetListAsync()
    {
        var customers = await _customerRepository.GetListAsync();
        var result = _mapper.Map<List<CustomerDto>>(customers); 
        return result;
    }
    // ID ile müşteri getirir
    public async Task<CustomerDto> GetAsync(Guid id)
    {
        var customer = await _customerRepository.GetAsync(id);
        var result = _mapper.Map<CustomerDto>(customer);
        return result;
    }
    // Yeni müşteri oluşturur
    [UnitOfWork]
    public async Task<CustomerDto> CreateAsync(CreateCustomerDto input)
    {
        var model = _mapper.Map<CreateCustomerModel>(input);
        model.UserId = input.UserId;
        var customer = await _customerManager.CreateAsync(model);
        var createdCustomer = await _customerRepository.InsertAsync(customer);
        return _mapper.Map<CustomerDto>(createdCustomer);   
    }
    // Müşteri bilgilerini günceller
    [UnitOfWork]
    public async Task<CustomerDto> UpdateAsync(Guid id, UpdateCustomerDto input)
    {
        var existingCustomer = await _customerRepository.GetAsync(id);
        var model = _mapper.Map<UpdateCustomerModel>(input);
        var updatedCustomer = await _customerManager.UpdateAsync(existingCustomer.Id, model);
        var savedCustomer = await _customerRepository.UpdateAsync(updatedCustomer, autoSave: true);
        return _mapper.Map<CustomerDto>(savedCustomer);
    }
    // Müşteriyi siler (Soft Delete)
    [UnitOfWork]
    public async Task DeleteAsync(Guid id)
    {
        var customer = await _customerRepository.GetAsync(id);
        await _customerRepository.DeleteAsync(customer, autoSave: true);
    }
}