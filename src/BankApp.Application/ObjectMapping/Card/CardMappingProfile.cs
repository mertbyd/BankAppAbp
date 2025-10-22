using AutoMapper;
using BankApp.Dtos.Card;
using BankApp.Models.Cards;
using BankApp.Entities;
using BankApp.Enums;
using System;

namespace BankApp.ObjectMapping;

public class CardMappingProfile : Profile
{
    public CardMappingProfile()
    {
        // DTO -> Model Eşlemeleri (Unmapped hataları çözüldü)
        CreateMap<CreateCardDto, CreateCardModel>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()); // Hata çözüldü
        
        CreateMap<UpdateCardDto, UpdateCardModel>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore()); // Hata çözüldü
            
        CreateMap<CardTransactionDto, CardTransactionModel>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CardId, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.TransactionDate, opt => opt.Ignore());
            
        CreateMap<CreditCardPaymentDto, CreditCardPaymentModel>()
            .ForMember(dest => dest.TargetCardId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.TransactionDate, opt => opt.Ignore());
        
        // Model -> Entity 
        CreateMap<CreateCardModel, Card>()
            // KRİTİK DÜZELTME: Id alanını yoksayıyoruz.
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            // ABP Auditing ve SoftDelete
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
            .ForMember(dest => dest.DeletionTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
            // Özel Eşlemeler
            .ForMember(dest => dest.CvvCode, opt => opt.MapFrom(src => src.CVV)) 
            .ForMember(dest => dest.AvailableLimit, opt => opt.MapFrom(src => src.CreditLimit)) 
            .ForMember(dest => dest.UsedLimit, opt => opt.MapFrom(src => 0));
        
        CreateMap<UpdateCardModel, Card>()
             // KRİTİK DÜZELTME: Id alanını yoksayıyoruz.
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            // Eksik Entity Alanlarını Yoksay (CardType, CardNumber, vb. Update'de güncellenmez)
            .ForMember(dest => dest.CardType, opt => opt.Ignore())
            .ForMember(dest => dest.CardNumber, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.AccountId, opt => opt.Ignore())
            .ForMember(dest => dest.AvailableLimit, opt => opt.Ignore()) 
            // ABP Auditing ve SoftDelete
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
            .ForMember(dest => dest.DeletionTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
            // Özel Eşlemeler
            .ForMember(dest => dest.CvvCode, opt => opt.MapFrom(src => src.CVV)) 
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
        // Entity -> DTO
        CreateMap<Card, CardDto>()
            .ForMember(dest => dest.CardHolderName, opt => opt.Ignore()) // Hata çözüldü
            .ForMember(dest => dest.CVV, opt => opt.MapFrom(src => src.CvvCode)) 
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status == CardStatuses.Active)) 
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreationTime)); 
        
        // Transaction mappings
        CreateMap<Transaction, CardTransactionResponseDto>()
            .ForMember(dest => dest.RemainingLimit, opt => opt.Ignore())
            .ForMember(dest => dest.AccountBalance, opt => opt.Ignore())
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.Id)); 
        
        CreateMap<Transaction, CreditCardPaymentResponseDto>()
            .ForMember(dest => dest.NewAvailableLimit, opt => opt.Ignore())
            .ForMember(dest => dest.NewUsedLimit, opt => opt.Ignore())
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.Id)); 
    }
}
