using AutoMapper;
using BankApp.Dtos.Auth;
using BankApp.Models.Auth;
using BankApp.Customers.Dtos;
using Volo.Abp.Identity;
using System;
using BankApp.Models.Customers;
using Volo.Abp.Guids;
using Volo.Abp.AutoMapper;

namespace BankApp.ObjectMapping.Auth;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        // Login mappings
        CreateMap<LoginDto, LoginModel>();
        CreateMap<LoginModel, LoginDto>();
        
        // Register DTO <-> Model
        CreateMap<RegisterDto, RegisterModel>();
        CreateMap<RegisterModel, RegisterDto>();
        
        // RegisterModel -> CreateCustomerModel
        CreateMap<RegisterModel, CreateCustomerModel>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
        
        // RegisterModel -> IdentityUser
        CreateMap<RegisterModel, IdentityUser>()
            .ConstructUsing((src, context) => 
            {
                var guidGenerator = (IGuidGenerator)context.Items["GuidGenerator"];
                return new IdentityUser(
                    guidGenerator.Create(),
                    src.UserName,
                    src.Email
                );
            })
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .IgnoreFullAuditedObjectProperties();
    }
}