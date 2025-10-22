using AutoMapper;
using BankApp.Dtos.Transaction;
using BankApp.Models.Transaction;
using BankApp.Entities;
using System;

namespace BankApp.ObjectMapping;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<CreateTransactionDto, CreateTransactionModel>();
        CreateMap<CreateTransactionModel, Transaction>()
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
            .ForMember(dest => dest.DeletionTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TransactionDate, 
                opt => opt.MapFrom(src => src.TransactionDate ?? DateTime.Now));
        CreateMap<Transaction, TransactionDto>();
    }
}