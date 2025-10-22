using FluentValidation;
using BankApp.Dtos.Card;
using BankApp; // BankAppDomainErrorCodes'a erişim için

namespace BankApp.FluentValidation.Card
{
    public class CreditCardPaymentDtoValidator : AbstractValidator<CreditCardPaymentDto>
    {
        public CreditCardPaymentDtoValidator()
        {
            // SourceCardId validation
            RuleFor(x => x.SourceCardId)
                .NotEmpty()
                .WithMessage(BankAppDomainErrorCodes.Cards.NotFound);
            // Amount validation (Tutar 0'dan büyük olmalı)
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage(BankAppDomainErrorCodes.Transactions.InvalidAmount);
            // Description validation (Açıklama uzunluğu)
            RuleFor(x => x.Description)
                .MaximumLength(200)
                .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed);
        }
    }
}