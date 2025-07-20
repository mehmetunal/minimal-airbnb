using FluentValidation;
using MinimalAirbnb.Application.DTOs.PropertyPhoto;

namespace MinimalAirbnb.Application.Validators.PropertyPhoto;

/// <summary>
/// Ev Fotoğrafı Güncelleme DTO Validatörü
/// </summary>
public class UpdatePropertyPhotoDtoValidator : AbstractValidator<UpdatePropertyPhotoDto>
{
    public UpdatePropertyPhotoDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Fotoğraf ID boş olamaz.");

        RuleFor(x => x.PhotoUrl)
            .MaximumLength(500).WithMessage("Fotoğraf URL'si 500 karakterden uzun olamaz.")
            .Must(BeValidUrl).WithMessage("Geçerli bir URL giriniz.")
            .When(x => !string.IsNullOrEmpty(x.PhotoUrl));

        RuleFor(x => x.Title)
            .MaximumLength(200).WithMessage("Fotoğraf başlığı 200 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.Title));

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