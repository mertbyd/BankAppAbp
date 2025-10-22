using AutoMapper;
using BankApp.Customers.Dtos;
using BankApp.Models.Customers;
using BankApp.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AutoMapper;

namespace BankApp.ObjectMapping;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        // DTO → Model
        CreateMap<CreateCustomerDto, CreateCustomerModel>();
        CreateMap<UpdateCustomerDto, UpdateCustomerModel>();
        
        // Model → Entity
        CreateMap<CreateCustomerModel, Customer>()
            .IgnoreFullAuditedObjectProperties();
        
        CreateMap<UpdateCustomerModel, Customer>()
            .IgnoreFullAuditedObjectProperties()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        // Entity → DTO
        CreateMap<Customer, CustomerDto>();
    }
}