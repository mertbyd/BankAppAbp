using FluentValidation;
using BankApp.Customers.Dtos;
using System;

namespace BankApp.FluentValidation.CustomerDtoValidation;

public class UpdateCustomerDtoValidator:AbstractValidator<UpdateCustomerDto>
{
    public UpdateCustomerDtoValidator()
    {
        // FullName validation
        RuleFor(x=>x.FullName).NotEmpty().WithMessage("FullName is required");
        // BirthPlace validation
        RuleFor(x=>x.Email).NotEmpty().WithMessage("Email is required");
        // RiskLimit validation
        RuleFor(x => x.RiskLimit)
            .GreaterThanOrEqualTo(0).WithMessage("Risk limiti 0'dan küçük olamaz");
        // PhoneNumber validation
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^(\+90|0)?[5][0-9]{9}$")
            .WithMessage("Geçerli bir Türkiye telefon numarası giriniz (05xxxxxxxxx)")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        // Email validation
        RuleFor(x=>x.Email).MaximumLength(500).WithMessage("Adres en fazla 500 karakter olabilir")
            .When(x=>!string.IsNullOrEmpty(x.Email));
    }
}