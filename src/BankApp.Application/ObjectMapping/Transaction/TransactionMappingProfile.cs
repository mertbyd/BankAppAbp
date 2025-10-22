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
        CreateMap<CreateTransactionModel, Transaction>();
        CreateMap<Transaction, TransactionDto>();
    }
}