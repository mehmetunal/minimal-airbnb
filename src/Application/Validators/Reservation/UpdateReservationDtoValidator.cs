using FluentValidation;
using MinimalAirbnb.Application.DTOs.Reservation;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Validators.Reservation;

/// <summary>
/// Rezervasyon Güncelleme DTO Validatörü
/// </summary>
public class UpdateReservationDtoValidator : AbstractValidator<UpdateReservationDto>
{
    public UpdateReservationDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Rezervasyon ID boş olamaz.");

        RuleFor(x => x.CheckInDate)
            .GreaterThan(DateTime.Today).WithMessage("Check-in tarihi bugünden sonra olmalıdır.")
            .LessThan(DateTime.Today.AddYears(2)).WithMessage("Check-in tarihi 2 yıldan sonra olamaz.");

        RuleFor(x => x.CheckOutDate)
            .GreaterThan(x => x.CheckInDate).WithMessage("Check-out tarihi check-in tarihinden sonra olmalıdır.")
            .LessThan(x => x.CheckInDate.AddDays(365)).WithMessage("Rezervasyon süresi 365 günden fazla olamaz.");

        RuleFor(x => x.GuestCount)
            .GreaterThan(0).WithMessage("Misafir sayısı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(50).WithMessage("Misafir sayısı 50'den fazla olamaz.");

        RuleFor(x => x.TotalPrice)
            .GreaterThan(0).WithMessage("Toplam fiyat 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(100000).WithMessage("Toplam fiyat 100.000'den fazla olamaz.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Geçerli bir durum seçiniz.");

        RuleFor(x => x.SpecialRequests)
            .MaximumLength(1000).WithMessage("Özel istekler 1000 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.SpecialRequests));

        RuleFor(x => x.CancellationReason)
            .MaximumLength(500).WithMessage("İptal nedeni 500 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.CancellationReason));
    }
} 