using FluentValidation;
using MinimalAirbnb.Application.Commands.Payment;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Validators.Payment;

/// <summary>
/// CreatePaymentCommand validator'ı
/// </summary>
public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(x => x.ReservationId)
            .NotEmpty()
            .WithMessage("Rezervasyon ID'si gereklidir.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("Kullanıcı ID'si gereklidir.");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Ödeme tutarı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(100000)
            .WithMessage("Ödeme tutarı 100.000 TL'den fazla olamaz.");

        RuleFor(x => x.Currency)
            .NotEmpty()
            .WithMessage("Para birimi gereklidir.")
            .Length(3)
            .WithMessage("Para birimi 3 karakter olmalıdır.");

        RuleFor(x => x.PaymentMethod)
            .IsInEnum()
            .WithMessage("Geçerli bir ödeme yöntemi seçiniz.");

        // Kredi/Banka kartı validasyonları
        When(x => x.PaymentMethod == PaymentMethod.CreditCard || x.PaymentMethod == PaymentMethod.DebitCard, () =>
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .WithMessage("Kart numarası gereklidir.")
                .Matches(@"^\d{4}\s\d{4}\s\d{4}\s\d{4}$")
                .WithMessage("Kart numarası geçerli formatta olmalıdır (1234 5678 9012 3456).");

            RuleFor(x => x.CardHolderName)
                .NotEmpty()
                .WithMessage("Kart sahibi adı gereklidir.")
                .MaximumLength(100)
                .WithMessage("Kart sahibi adı 100 karakterden uzun olamaz.");

            RuleFor(x => x.ExpiryDate)
                .NotEmpty()
                .WithMessage("Son kullanma tarihi gereklidir.")
                .Matches(@"^\d{2}/\d{2}$")
                .WithMessage("Son kullanma tarihi MM/YY formatında olmalıdır.");

            RuleFor(x => x.CVV)
                .NotEmpty()
                .WithMessage("CVV gereklidir.")
                .Matches(@"^\d{3,4}$")
                .WithMessage("CVV 3 veya 4 haneli olmalıdır.");
        });

        // PayPal validasyonları
        When(x => x.PaymentMethod == PaymentMethod.PayPal, () =>
        {
            RuleFor(x => x.PayPalEmail)
                .NotEmpty()
                .WithMessage("PayPal email adresi gereklidir.")
                .EmailAddress()
                .WithMessage("Geçerli bir email adresi giriniz.");
        });

        // Banka transferi validasyonları
        When(x => x.PaymentMethod == PaymentMethod.BankTransfer, () =>
        {
            RuleFor(x => x.BankAccountNumber)
                .NotEmpty()
                .WithMessage("Banka hesap numarası gereklidir.")
                .Matches(@"^\d{10,26}$")
                .WithMessage("Geçerli bir banka hesap numarası giriniz.");
        });

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Açıklama 500 karakterden uzun olamaz.");

        RuleFor(x => x.CallbackUrl)
            .Must(BeValidUrl)
            .When(x => !string.IsNullOrEmpty(x.CallbackUrl))
            .WithMessage("Geçerli bir callback URL giriniz.");
    }



    private bool BeValidUrl(string? url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
} 