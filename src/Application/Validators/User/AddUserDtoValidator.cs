using FluentValidation;
using MinimalAirbnb.Application.DTOs.User;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Validators.User;

/// <summary>
/// Kullanıcı Ekleme DTO Validatörü
/// </summary>
public class AddUserDtoValidator : AbstractValidator<AddUserDto>
{
    public AddUserDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email adresi boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
            .MaximumLength(256).WithMessage("Email adresi 256 karakterden uzun olamaz.");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
            .Length(3, 50).WithMessage("Kullanıcı adı 3-50 karakter arasında olmalıdır.")
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Kullanıcı adı sadece harf, rakam ve alt çizgi içerebilir.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad boş olamaz.")
            .Length(2, 50).WithMessage("Ad 2-50 karakter arasında olmalıdır.")
            .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ\\s]+$").WithMessage("Ad sadece harf içerebilir.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad boş olamaz.")
            .Length(2, 50).WithMessage("Soyad 2-50 karakter arasında olmalıdır.")
            .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ\\s]+$").WithMessage("Soyad sadece harf içerebilir.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
            .Matches("^[0-9+\\-\\s()]+$").WithMessage("Geçerli bir telefon numarası giriniz.")
            .Length(10, 15).WithMessage("Telefon numarası 10-15 karakter arasında olmalıdır.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş olamaz.")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
            .MaximumLength(100).WithMessage("Şifre 100 karakterden uzun olamaz.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Now.AddYears(-18)).WithMessage("Kullanıcı 18 yaşından büyük olmalıdır.")
            .GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Geçerli bir doğum tarihi giriniz.")
            .When(x => x.DateOfBirth.HasValue);

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Geçerli bir cinsiyet seçiniz.")
            .When(x => x.Gender.HasValue);

        RuleFor(x => x.UserType)
            .IsInEnum().WithMessage("Geçerli bir kullanıcı tipi seçiniz.");

        RuleFor(x => x.Address)
            .MaximumLength(500).WithMessage("Adres 500 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.Address));

        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("Şehir 100 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.City));

        RuleFor(x => x.Country)
            .MaximumLength(100).WithMessage("Ülke 100 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.Country));

        RuleFor(x => x.PostalCode)
            .MaximumLength(20).WithMessage("Posta kodu 20 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.PostalCode));

        RuleFor(x => x.Bio)
            .MaximumLength(1000).WithMessage("Biyografi 1000 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.Bio));

        RuleFor(x => x.ProfilePictureUrl)
            .MaximumLength(500).WithMessage("Profil resmi URL'si 500 karakterden uzun olamaz.")
            .When(x => !string.IsNullOrEmpty(x.ProfilePictureUrl));
    }
} 