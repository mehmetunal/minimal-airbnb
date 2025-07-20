using FluentValidation;
using MinimalAirbnb.Application.DTOs.PropertyPhoto;

namespace MinimalAirbnb.Application.Validators.PropertyPhoto;

/// <summary>
/// Ev Fotoğrafı Ekleme DTO Validatörü
/// </summary>
public class AddPropertyPhotoDtoValidator : AbstractValidator<AddPropertyPhotoDto>
{
    public AddPropertyPhotoDtoValidator()
    {
        RuleFor(x => x.PropertyId)
            .NotEmpty().WithMessage("Ev ID boş olamaz.");

        RuleFor(x => x.PhotoUrl)
            .NotEmpty().WithMessage("Fotoğraf URL'si boş olamaz.")
            .MaximumLength(500).WithMessage("Fotoğraf URL'si 500 karakterden uzun olamaz.")
            .Must(BeValidUrl).WithMessage("Geçerli bir URL giriniz.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Fotoğraf başlığı boş olamaz.")
            .MaximumLength(200).WithMessage("Fotoğraf başlığı 200 karakterden uzun olamaz.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Fotoğraf açıklaması 500 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0).WithMessage("Sıralama 0'dan küçük olamaz.");
    }

    private static bool BeValidUrl(string? url)
    {
        if (string.IsNullOrEmpty(url))
            return false;

        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
} 