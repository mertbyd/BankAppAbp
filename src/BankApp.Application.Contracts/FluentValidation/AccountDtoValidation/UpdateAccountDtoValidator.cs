using FluentValidation;
using BankApp.Dtos.Accounts;
using BankApp; // BankAppDomainErrorCodes'a erişim için

namespace BankApp.FluentValidation.AccountDtoValidation;

public class UpdateAccountDtoValidator : AbstractValidator<UpdateAccountDto>
{
    public UpdateAccountDtoValidator()
    {
        RuleFor(x => x.AccountName)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .Length(2, 100)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed );

        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0)
            .WithMessage(BankAppDomainErrorCodes.Transactions.InvalidAmount );
    }
}