using FluentValidation;
using BankApp.Enums;
using BankApp.Dtos.Card;
using BankApp; // BankAppDomainErrorCodes'a erişim için

namespace BankApp.FluentValidation.CardDtoValidation;

public class CardTransactionDtoValidator : AbstractValidator<CardTransactionDto>
{
    public CardTransactionDtoValidator()
    {
        // Amount validation (Tutar 0'dan büyük olmalı)
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage(BankAppDomainErrorCodes.Transactions.InvalidAmount)
            .LessThanOrEqualTo(50000)
            .WithMessage(BankAppDomainErrorCodes.Transactions.AmountLimitExceeded);
        // Description validation (Açıklama uzunluğu)
        RuleFor(x => x.Description)
            .MaximumLength(200)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed);
        // TransactionType validation (Enum geçerliliği)
        RuleFor(x => x.TransactionType)
            .IsInEnum() 
            .WithMessage(BankAppDomainErrorCodes.General.InvalidOperation); 
        
    }
}