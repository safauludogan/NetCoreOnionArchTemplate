﻿
using FluentValidation;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen ürün adını boş geçmeyiniz.")
                .MaximumLength(150)
                .MinimumLength(5)
                    .WithMessage("Lütfen ürün adını 5-150 karakter arasında giriniz");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen stok bilgisini giriniz")
                .Must(s => s >= 0)
                    .WithMessage("Stok bilgisi sıfırdan küçük olamaz");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen fiyat bilgisini boş geçmeyiniz")
                .Must(s => s >= 0)
                    .WithMessage("Fiyat bilgisi negatif olamaz!");
        }
    }
}
