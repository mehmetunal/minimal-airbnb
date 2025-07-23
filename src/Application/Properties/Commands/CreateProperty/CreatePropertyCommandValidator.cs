using FluentValidation;
using MinimalAirbnb.Application.Properties.Commands.CreateProperty;

namespace MinimalAirbnb.Application.Properties.Commands.CreateProperty;

/// <summary>
/// CreatePropertyCommand validator'ı
/// </summary>
public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MaximumLength(200).WithMessage("Başlık 200 karakterden uzun olamaz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MaximumLength(2000).WithMessage("Açıklama 2000 karakterden uzun olamaz.");

        RuleFor(x => x.PropertyType)
            .NotEmpty().WithMessage("Property tipi boş olamaz.");

        RuleFor(x => x.PricePerNight)
            .GreaterThan(0).WithMessage("Gecelik fiyat 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(10000).WithMessage("Gecelik fiyat 10000'den büyük olamaz.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Adres boş olamaz.")
            .MaximumLength(500).WithMessage("Adres 500 karakterden uzun olamaz.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Şehir boş olamaz.")
            .MaximumLength(100).WithMessage("Şehir 100 karakterden uzun olamaz.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Ülke boş olamaz.")
            .MaximumLength(100).WithMessage("Ülke 100 karakterden uzun olamaz.");

        RuleFor(x => x.PostalCode)
            .MaximumLength(20).WithMessage("Posta kodu 20 karakterden uzun olamaz.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Enlem -90 ile 90 arasında olmalıdır.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Boylam -180 ile 180 arasında olmalıdır.");

        RuleFor(x => x.Bedrooms)
            .GreaterThan(0).WithMessage("Yatak odası sayısı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(20).WithMessage("Yatak odası sayısı 20'den büyük olamaz.");

        RuleFor(x => x.Bathrooms)
            .GreaterThan(0).WithMessage("Banyo sayısı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(10).WithMessage("Banyo sayısı 10'dan büyük olamaz.");

        RuleFor(x => x.MaxGuests)
            .GreaterThan(0).WithMessage("Maksimum misafir sayısı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(20).WithMessage("Maksimum misafir sayısı 20'den büyük olamaz.");

        RuleFor(x => x.MinimumStay)
            .GreaterThan(0).WithMessage("Minimum konaklama süresi 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(30).WithMessage("Minimum konaklama süresi 30 günden uzun olamaz.");

        RuleFor(x => x.MaximumStay)
            .GreaterThan(0).WithMessage("Maksimum konaklama süresi 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(365).WithMessage("Maksimum konaklama süresi 365 günden uzun olamaz.");

        RuleFor(x => x.MaximumStay)
            .GreaterThanOrEqualTo(x => x.MinimumStay)
            .WithMessage("Maksimum konaklama süresi minimum konaklama süresinden küçük olamaz.");
    }
} 