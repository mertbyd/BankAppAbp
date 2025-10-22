using FluentValidation;
using BankApp.Dtos.Accounts;
using BankApp; // BankAppDomainErrorCodes'u kullanmak için ekledik

namespace BankApp.FluentValidation.AccountDtoValidation;

public class CreateAccountDtoValidator : AbstractValidator<CreateAccountDto>
{
    public CreateAccountDtoValidator()
    {
        // AccountName validation
        RuleFor(x => x.AccountName)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed ) 
            .Length(2, 100)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed );
        // AccountNumber validation
        RuleFor(x => x.AccountNumber)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed );
        // IBAN validation
        RuleFor(x => x.IBAN)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .Length(26)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed );
        // CustomerId validation
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.Customers.NotFound);
        // InitialBalance validation
        RuleFor(x => x.InitialBalance)
            .GreaterThanOrEqualTo(0)
            .WithMessage(BankAppDomainErrorCodes.Transactions.InvalidAmount);
    }
}