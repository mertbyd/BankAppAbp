using AutoMapper;
using BankApp.Dtos.Card;
using BankApp.Models.Cards;
using BankApp.Entities;
using BankApp.Enums;
using System;
using Volo.Abp.AutoMapper;

namespace BankApp.ObjectMapping;

public class CardMappingProfile : Profile
{
    public CardMappingProfile()
    {
        // DTO -> Model Eşlemeleri
        CreateMap<CreateCardDto, CreateCardModel>();
        CreateMap<UpdateCardDto, UpdateCardModel>();
        CreateMap<CardTransactionDto, CardTransactionModel>();
        CreateMap<CreditCardPaymentDto, CreditCardPaymentModel>();
        // Model -> Entity 
        CreateMap<CreateCardModel, Card>()
            .ForMember(dest => dest.CvvCode, opt => opt.MapFrom(src => src.CVV))
            .ForMember(dest => dest.AvailableLimit, opt => opt.MapFrom(src => src.CreditLimit))
            .ForMember(dest => dest.UsedLimit, opt => opt.MapFrom(src => 0))
            .IgnoreFullAuditedObjectProperties();
        CreateMap<UpdateCardModel, Card>()
            .ForMember(dest => dest.CvvCode, opt => opt.MapFrom(src => src.CVV))
            .IgnoreFullAuditedObjectProperties()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        // Entity -> DTO
        CreateMap<Card, CardDto>()
            .ForMember(dest => dest.CVV, opt => opt.MapFrom(src => src.CvvCode))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status == CardStatuses.Active))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreationTime));
        
        // Transaction mappings
        CreateMap<Transaction, CardTransactionResponseDto>()
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.Id));
        
        CreateMap<Transaction, CreditCardPaymentResponseDto>()
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.Id));
    }
}