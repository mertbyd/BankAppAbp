using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankApp.Customers.Dtos;
using Volo.Abp.Application.Services;

namespace BankApp.Interface;

public interface ICustomerAppService : IApplicationService
{
    // Tüm müşterileri getirir
    Task<List<CustomerDto>> GetListAsync();
    
    // ID ile müşteri getirir
    Task<CustomerDto> GetAsync(Guid id);
    
    // Yeni müşteri oluşturur
    Task<CustomerDto> CreateAsync(CreateCustomerDto input);
    
    // Müşteri bilgilerini günceller
    Task<CustomerDto> UpdateAsync(Guid id, UpdateCustomerDto input);
    
    // Müşteriyi siler
    Task DeleteAsync(Guid id);
}