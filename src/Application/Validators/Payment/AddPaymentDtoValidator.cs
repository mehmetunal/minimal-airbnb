using FluentValidation;
using MinimalAirbnb.Application.DTOs.Payment;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Validators.Payment;

/// <summary>
/// Ödeme Ekleme DTO Validatörü
/// </summary>
public class AddPaymentDtoValidator : AbstractValidator<AddPaymentDto>
{
    public AddPaymentDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");

        RuleFor(x => x.ReservationId)
            .NotEmpty().WithMessage("Rezervasyon ID boş olamaz.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Ödeme tutarı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(100000).WithMessage("Ödeme tutarı 100.000'den fazla olamaz.");

        RuleFor(x => x.PaymentMethod)
            .IsInEnum().WithMessage("Geçerli bir ödeme yöntemi seçiniz.");

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