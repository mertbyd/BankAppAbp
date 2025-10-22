using FluentValidation;
using BankApp.Dtos.Auth;
using BankApp; // BankAppDomainErrorCodes'a erişim için

namespace BankApp.FluentValidation.AuthDtoValidation;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        // UserName validation
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .Length(3, 20)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed);
        // Email validation
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .EmailAddress()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed);
        // Password validation
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .MinimumLength(6)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed);
        // FullName validation
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .Length(3, 100)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed);
        RuleFor(x => x.TcNumber)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .Length(11)
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed)
            .Matches("^[0-9]{11}$")
            .WithMessage(BankAppDomainErrorCodes.General.ValidationFailed);
    }
}
