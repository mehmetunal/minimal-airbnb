using FluentValidation;
using MinimalAirbnb.Application.DTOs.Payment;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Validators.Payment;

/// <summary>
/// Ödeme Güncelleme DTO Validatörü
/// </summary>
public class UpdatePaymentDtoValidator : AbstractValidator<UpdatePaymentDto>
{
    public UpdatePaymentDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Ödeme ID boş olamaz.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Ödeme tutarı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(100000).WithMessage("Ödeme tutarı 100.000'den fazla olamaz.");

        RuleFor(x => x.PaymentMethod)
            .MaximumLength(50).WithMessage("Ödeme yöntemi 50 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.PaymentMethod));

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Geçerli bir durum seçiniz.");

        RuleFor(x => x.TransactionId)
            .MaximumLength(100).WithMessage("İşlem ID'si 100 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.TransactionId));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Açıklama 500 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
} 