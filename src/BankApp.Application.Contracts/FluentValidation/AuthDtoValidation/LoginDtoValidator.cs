using FluentValidation;
using BankApp.Dtos.Auth;
using BankApp; // BankAppDomainErrorCodes'a erişim için

namespace BankApp.FluentValidation.AuthDtoValidation;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        // UserName validation
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.Auth.InvalidCredentials);
        // Password validation
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(BankAppDomainErrorCodes.Auth.InvalidCredentials);
    }
}