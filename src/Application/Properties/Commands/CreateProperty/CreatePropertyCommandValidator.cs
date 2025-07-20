using FluentValidation;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Properties.Commands.CreateProperty;

/// <summary>
/// CreatePropertyCommand için validator
/// </summary>
public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz")
            .MaximumLength(200).WithMessage("Başlık 200 karakterden uzun olamaz");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz")
            .MaximumLength(2000).WithMessage("Açıklama 2000 karakterden uzun olamaz");

        RuleFor(x => x.PropertyType)
            .IsInEnum().WithMessage("Geçersiz ev tipi");

        RuleFor(x => x.BedroomCount)
            .GreaterThan(0).WithMessage("Oda sayısı 0'dan büyük olmalıdır")
            .LessThanOrEqualTo(20).WithMessage("Oda sayısı 20'den fazla olamaz");

        RuleFor(x => x.BedCount)
            .GreaterThan(0).WithMessage("Yatak sayısı 0'dan büyük olmalıdır")
            .LessThanOrEqualTo(20).WithMessage("Yatak sayısı 20'den fazla olamaz");

        RuleFor(x => x.BathroomCount)
            .GreaterThan(0).WithMessage("Banyo sayısı 0'dan büyük olmalıdır")
            .LessThanOrEqualTo(10).WithMessage("Banyo sayısı 10'dan fazla olamaz");

        RuleFor(x => x.MaxGuestCount)
            .GreaterThan(0).WithMessage("Maksimum misafir sayısı 0'dan büyük olmalıdır")
            .LessThanOrEqualTo(20).WithMessage("Maksimum misafir sayısı 20'den fazla olamaz");

        RuleFor(x => x.PricePerNight)
            .GreaterThan(0).WithMessage("Günlük fiyat 0'dan büyük olmalıdır")
            .LessThanOrEqualTo(10000).WithMessage("Günlük fiyat 10.000 TL'den fazla olamaz");

        RuleFor(x => x.CleaningFee)
            .GreaterThanOrEqualTo(0).WithMessage("Temizlik ücreti negatif olamaz")
            .LessThanOrEqualTo(1000).WithMessage("Temizlik ücreti 1.000 TL'den fazla olamaz");

        RuleFor(x => x.ServiceFee)
            .GreaterThanOrEqualTo(0).WithMessage("Hizmet ücreti negatif olamaz")
            .LessThanOrEqualTo(1000).WithMessage("Hizmet ücreti 1.000 TL'den fazla olamaz");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Adres boş olamaz")
            .MaximumLength(500).WithMessage("Adres 500 karakterden uzun olamaz");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Şehir boş olamaz")
            .MaximumLength(100).WithMessage("Şehir 100 karakterden uzun olamaz");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Ülke boş olamaz")
            .MaximumLength(100).WithMessage("Ülke 100 karakterden uzun olamaz");

        RuleFor(x => x.PostalCode)
            .MaximumLength(20).WithMessage("Posta kodu 20 karakterden uzun olamaz");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Enlem -90 ile 90 arasında olmalıdır");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Boylam -180 ile 180 arasında olmalıdır");

        RuleFor(x => x.MinStayDays)
            .GreaterThan(0).WithMessage("Minimum konaklama süresi 0'dan büyük olmalıdır")
            .LessThanOrEqualTo(30).WithMessage("Minimum konaklama süresi 30 günden fazla olamaz");

        RuleFor(x => x.MaxStayDays)
            .GreaterThan(0).WithMessage("Maksimum konaklama süresi 0'dan büyük olmalıdır")
            .LessThanOrEqualTo(365).WithMessage("Maksimum konaklama süresi 365 günden fazla olamaz")
            .GreaterThanOrEqualTo(x => x.MinStayDays).WithMessage("Maksimum konaklama süresi minimum süreden küçük olamaz");

        RuleFor(x => x.CancellationDays)
            .GreaterThanOrEqualTo(0).WithMessage("İptal süresi negatif olamaz")
            .LessThanOrEqualTo(30).WithMessage("İptal süresi 30 günden fazla olamaz");
    }
} 