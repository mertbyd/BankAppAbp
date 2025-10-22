using AutoMapper;
using BankApp.Customers.Dtos;
using BankApp.Models.Customers;
using BankApp.Entities;
using Volo.Abp.Application.Dtos; // ExtraProperties için gerekli

namespace BankApp.ObjectMapping;

public class CustomerMappingProfile:Profile
{
    public CustomerMappingProfile()
    {
        // DTO → Model
        CreateMap<CreateCustomerDto, CreateCustomerModel>();
        CreateMap<UpdateCustomerDto, UpdateCustomerModel>(); 
        CreateMap<CreateCustomerModel, Customer>()
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
            .ForMember(dest => dest.DeletionTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.RiskLimit, opt => opt.Ignore());
        CreateMap<UpdateCustomerModel, Customer>()
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
            .ForMember(dest => dest.DeletionTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // 
            .ForMember(dest => dest.TcNumber, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore());
    }
}
