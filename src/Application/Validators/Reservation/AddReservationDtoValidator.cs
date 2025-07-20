using FluentValidation;
using MinimalAirbnb.Application.DTOs.Reservation;

namespace MinimalAirbnb.Application.Validators.Reservation;

/// <summary>
/// Rezervasyon Ekleme DTO Validatörü
/// </summary>
public class AddReservationDtoValidator : AbstractValidator<AddReservationDto>
{
    public AddReservationDtoValidator()
    {
        RuleFor(x => x.GuestId)
            .NotEmpty().WithMessage("Misafir ID boş olamaz.");

        RuleFor(x => x.PropertyId)
            .NotEmpty().WithMessage("Ev ID boş olamaz.");

        RuleFor(x => x.CheckInDate)
            .NotEmpty().WithMessage("Check-in tarihi boş olamaz.")
            .GreaterThan(DateTime.Today).WithMessage("Check-in tarihi bugünden sonra olmalıdır.")
            .LessThan(DateTime.Today.AddYears(2)).WithMessage("Check-in tarihi 2 yıldan sonra olamaz.");

        RuleFor(x => x.CheckOutDate)
            .NotEmpty().WithMessage("Check-out tarihi boş olamaz.")
            .GreaterThan(x => x.CheckInDate).WithMessage("Check-out tarihi check-in tarihinden sonra olmalıdır.")
            .LessThan(x => x.CheckInDate.AddDays(365)).WithMessage("Rezervasyon süresi 365 günden fazla olamaz.");

        RuleFor(x => x.GuestCount)
            .GreaterThan(0).WithMessage("Misafir sayısı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(50).WithMessage("Misafir sayısı 50'den fazla olamaz.");

        RuleFor(x => x.TotalPrice)
            .GreaterThan(0).WithMessage("Toplam fiyat 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(100000).WithMessage("Toplam fiyat 100.000'den fazla olamaz.");

        RuleFor(x => x.SpecialRequests)
            .MaximumLength(1000).WithMessage("Özel istekler 1000 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.SpecialRequests));
    }
} 