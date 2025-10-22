using AutoMapper;
using BankApp.Dtos.Accounts;
using BankApp.Models.Accounts;
using BankApp.Entities;
using System;
using Volo.Abp.AutoMapper; // Bunu ekleyin!

namespace BankApp.ObjectMapping;

public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        CreateMap<CreateAccountDto, CreateAccountModel>();
        CreateMap<UpdateAccountDto, UpdateAccountModel>();
        CreateMap<CreateAccountModel, Account>()
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.InitialBalance))
            .IgnoreFullAuditedObjectProperties();
        CreateMap<UpdateAccountModel, Account>()
            .IgnoreFullAuditedObjectProperties();
        CreateMap<Account, AccountDto>();
    }
}