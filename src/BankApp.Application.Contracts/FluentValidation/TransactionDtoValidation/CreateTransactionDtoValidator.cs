using FluentValidation;
using System;
using BankApp.Dtos.Transaction;
using BankApp.Enums;
namespace BankApp.FluentValidation.TransactionDtoValidation;

public class CreateTransactionDtoValidator:AbstractValidator<CreateTransactionDto>
{
    public CreateTransactionDtoValidator()
    {
        //TransactionType validation
        RuleFor(x => x.TransactionTypesId)
            .IsInEnum().WithMessage("Geçerli bir işlem tipi seçin");
        //CustomerId validation
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Müşteri ID boş olamaz");
        //Amount validation
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("İşlem tutarı 0'dan büyük olmalı");
        //Deposit işlemi için hedef hesap zorunlu
        When(x => x.TransactionTypesId == TransactionTypes.Deposit, () =>
        {
            RuleFor(x=>x.TransactionTypesId)
                .NotNull().WithMessage("Para yatırma işlemi için hedef hesap zorunludur");
        });
        //Withdraw işlemi için kaynak hesap zorunlu
        When(x => x.TransactionTypesId == TransactionTypes.Withdraw, () =>
        {
            RuleFor(x=>x.TransactionTypesId)
                .NotNull().WithMessage("Para yatırma işlemi için hedef hesap zorunludur");
        });
        //Purchase ve Payment işlemleri için kart zorunlu
        When(x => x.TransactionTypesId == TransactionTypes.Purchase || 
                  x.TransactionTypesId == TransactionTypes.Payment, () =>
        {
            RuleFor(x => x.CardId)
                .NotNull().WithMessage("Harcama ve ödeme işlemleri için kart zorunludur");
        });
        //TransactionDate validation
        RuleFor(x => x.TransactionDate)
            .LessThanOrEqualTo(DateTime.Now.AddMinutes(5))
            .WithMessage("İşlem tarihi gelecekte olamaz")
            .When(x => x.TransactionDate.HasValue);

    }
}