using FluentValidation;
using System;
using BankApp.Enums;
using BankApp.Dtos.Card;
using BankApp; // BankAppDomainErrorCodes'a erişim için

namespace BankApp.FluentValidation.CardDtoValidation;

public class CreateCardDtoValidator : AbstractValidator<CreateCardDto>
{
    public CreateCardDtoValidator()
    {
        // CardType validation
        RuleFor(x => x.CardType)
            .IsInEnum()
            .WithMessage(BankAppDomainErrorCodes.Cards.InvalidCardType);

        // CardNumber validation
        RuleFor(x => x.CardNumber)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .Length(16)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .Matches(@"^\d{16}$")
            .WithErrorCode(BankAppDomainErrorCodes.General.ValidationFailed)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed);

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

        // CustomerId validation
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.Customers.NotFound);
        // Credit card için özel kurallar
        When(x => x.CardType == CardType.Credit, () =>
        {
            RuleFor(x => x.CreditLimit)
                .NotNull()
                .WithMessage(BankAppDomainErrorCodes.Cards.InsufficientLimit)
                .GreaterThan(0)
                .WithMessage(BankAppDomainErrorCodes.Cards.InsufficientLimit);
        });
        When(x => x.CardType == CardType.Debit, () =>
        {
            RuleFor(x => x.AccountId)
                .NotNull()
                .WithMessage(BankAppDomainErrorCodes.Accounts.NotFound);
        });
    }
}