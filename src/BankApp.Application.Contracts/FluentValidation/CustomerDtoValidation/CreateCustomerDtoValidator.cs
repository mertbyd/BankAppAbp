using FluentValidation;
 using BankApp.Customers.Dtos;
namespace MyAbpProjectNet7.FluentValidation.CustomerDtoValidation;

public class CreateCustomerDtoValidator:AbstractValidator<CreateCustomerDto> 
{
    public CreateCustomerDtoValidator()
    {
        // FullName validation
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ad Soyad boş olamaz");
        // TcNumber validation
        RuleFor(x => x.TcNumber).NotEmpty().WithMessage("TC Kimlik No boş olamaz")
            .Length(11).WithMessage("TC Kimlik No 11 haneli olmalı")
            .Matches(@"^\d{11}$").WithMessage("TC Kimlik No sadece rakam içermeli");
        // BirthPlace validation
        RuleFor(x => x.BirthPlace)
            .NotEmpty().WithMessage("Doğum yeri boş olamaz");
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^(\+90|0)?[5][0-9]{9}$")
            .WithMessage("Geçerli bir Türkiye telefon numarası giriniz (05xxxxxxxxx)")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        RuleFor(x => x.Address)
            .MaximumLength(500).WithMessage("Adres en fazla 500 karakter olabilir");
    }
}