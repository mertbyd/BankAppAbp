using FluentValidation;
using BankApp.Enums;
using System;
using BankApp.Dtos.Card;
using BankApp; // BankAppDomainErrorCodes'a erişim için

namespace BankApp.FluentValidation.CardDtoValidation;

public class UpdateCardDtoValidator : AbstractValidator<UpdateCardDto>
{
    public UpdateCardDtoValidator()
    {
        // CardHolderName validation
        RuleFor(x => x.CardHolderName)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .Length(2, 50)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed);
        // CVV validation
        RuleFor(x => x.CVV)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .Length(3, 4)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .Matches(@"^\d{3,4}$")
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed);
        // ExpiryMonth validation
        RuleFor(x => x.ExpiryMonth)
            .InclusiveBetween(1, 12)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed);
        // ExpiryYear validation
        RuleFor(x => x.ExpiryYear)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .LessThanOrEqualTo(DateTime.Now.Year + 10)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed);
        // Status validation
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed);

        // CreditLimit validation (Credit card için)
        RuleFor(x => x.CreditLimit)
            .GreaterThan(0)
            .WithMessage(BankAppDomainErrorCodes.Cards.InsufficientLimit)
            .When(x => x.CreditLimit.HasValue);
        // UsedLimit validation
        RuleFor(x => x.UsedLimit)
            .GreaterThanOrEqualTo(0)
            .WithMessage(BankAppDomainErrorCodes.Transactions.InvalidAmount);
        // UsedLimit CreditLimit'i aşmasın
        RuleFor(x => x.UsedLimit)
            .LessThanOrEqualTo(x => x.CreditLimit ?? 0)
            .WithMessage(BankAppDomainErrorCodes.Cards.InsufficientLimit)
            .When(x => x.CreditLimit.HasValue && x.CreditLimit > 0);
    }
}