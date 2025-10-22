using AutoMapper;
using BankApp.Dtos.Accounts;
using BankApp.Models.Accounts;
using BankApp.Entities;
using System;

namespace BankApp.ObjectMapping;

public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        // DTO -> Model (birebir aynı)
        CreateMap<CreateAccountDto, CreateAccountModel>();
        CreateMap<UpdateAccountDto, UpdateAccountModel>();
        // Model -> Entity
        CreateMap<CreateAccountModel, Account>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Balance, opt => opt.Ignore())
            .ForMember(dest => dest.ClosedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
            .ForMember(dest => dest.DeletionTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
            .AfterMap((src, dest) => 
            {
                if (dest.OpenedAt == default)
                    dest.OpenedAt = DateTime.Now;
            });
        CreateMap<UpdateAccountModel, Account>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.AccountNumber, opt => opt.Ignore())
            .ForMember(dest => dest.IBAN, opt => opt.Ignore())
            .ForMember(dest => dest.OpenedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
            .ForMember(dest => dest.DeletionTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Account, AccountDto>();
    }
}