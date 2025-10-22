using AutoMapper;
using BankApp.Dtos.Auth;
using BankApp.Models.Auth;
using BankApp.Customers.Dtos;
using Volo.Abp.Identity;
using System;
using BankApp.Models.Customers;
using Volo.Abp.Guids;

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
        // ÖNEMLİ: RegisterModel -> IdentityUser mapping - EKSİK OLAN BU!
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
            .ForMember(dest => dest.Surname, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
            .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
            .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
            .ForMember(dest => dest.IsExternal, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
            .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
            .ForMember(dest => dest.Roles, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.Claims, opt => opt.Ignore())
            .ForMember(dest => dest.Logins, opt => opt.Ignore())
            .ForMember(dest => dest.Tokens, opt => opt.Ignore())
            .ForMember(dest => dest.OrganizationUnits, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
            .ForMember(dest => dest.DeletionTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
            .ForMember(dest => dest.ExtraProperties, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
            .ForMember(dest => dest.TenantId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}