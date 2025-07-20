using FluentValidation;
using MinimalAirbnb.Application.DTOs.Property;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Validators.Property;

/// <summary>
/// Ev Güncelleme DTO Validatörü
/// </summary>
public class UpdatePropertyDtoValidator : AbstractValidator<UpdatePropertyDto>
{
    public UpdatePropertyDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Ev ID boş olamaz.");

        RuleFor(x => x.Title)
            .Length(5, 200).WithMessage("Ev başlığı 5-200 karakter arasında olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.Title));

        RuleFor(x => x.Description)
            .Length(10, 2000).WithMessage("Ev açıklaması 10-2000 karakter arasında olmalıdır.")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.PropertyType)
            .IsInEnum().WithMessage("Geçerli bir ev tipi seçiniz.");

        RuleFor(x => x.Address)
            .Length(10, 500).WithMessage("Adres 10-500 karakter arasında olmalıdır.");

        RuleFor(x => x.City)
            .Length(2, 100).WithMessage("Şehir 2-100 karakter arasında olmalıdır.");

        RuleFor(x => x.Country)
            .Length(2, 100).WithMessage("Ülke 2-100 karakter arasında olmalıdır.");

        RuleFor(x => x.PostalCode)
            .Length(3, 20).WithMessage("Posta kodu 3-20 karakter arasında olmalıdır.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Enlem -90 ile 90 arasında olmalıdır.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Boylam -180 ile 180 arasında olmalıdır.");

        RuleFor(x => x.PricePerNight)
            .GreaterThan(0).WithMessage("Gecelik fiyat 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(10000).WithMessage("Gecelik fiyat 10.000'den fazla olamaz.");

        RuleFor(x => x.CleaningFee)
            .GreaterThanOrEqualTo(0).WithMessage("Temizlik ücreti negatif olamaz.")
            .LessThanOrEqualTo(1000).WithMessage("Temizlik ücreti 1.000'den fazla olamaz.");

        RuleFor(x => x.ServiceFee)
            .GreaterThanOrEqualTo(0).WithMessage("Hizmet ücreti negatif olamaz.")
            .LessThanOrEqualTo(500).WithMessage("Hizmet ücreti 500'den fazla olamaz.");

        RuleFor(x => x.Bedrooms)
            .GreaterThan(0).WithMessage("Yatak odası sayısı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(20).WithMessage("Yatak odası sayısı 20'den fazla olamaz.");

        RuleFor(x => x.Beds)
            .GreaterThan(0).WithMessage("Yatak sayısı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(50).WithMessage("Yatak sayısı 50'den fazla olamaz.");

        RuleFor(x => x.Bathrooms)
            .GreaterThan(0).WithMessage("Banyo sayısı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(20).WithMessage("Banyo sayısı 20'den fazla olamaz.");

        RuleFor(x => x.MaxGuests)
            .GreaterThan(0).WithMessage("Maksimum misafir sayısı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(50).WithMessage("Maksimum misafir sayısı 50'den fazla olamaz.");

        RuleFor(x => x.MinimumStay)
            .GreaterThan(0).WithMessage("Minimum konaklama süresi 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(365).WithMessage("Minimum konaklama süresi 365 günden fazla olamaz.");

        RuleFor(x => x.MaximumStay)
            .GreaterThan(0).WithMessage("Maksimum konaklama süresi 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(365).WithMessage("Maksimum konaklama süresi 365 günden fazla olamaz.")
            .GreaterThanOrEqualTo(x => x.MinimumStay).WithMessage("Maksimum konaklama süresi minimum konaklama süresinden küçük olamaz.");

        RuleFor(x => x.HouseRules)
            .MaximumLength(1000).WithMessage("Ev kuralları 1000 karakterden uzun olamaz.");
    }
} 