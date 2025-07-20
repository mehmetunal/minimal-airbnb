using FluentValidation;
using MinimalAirbnb.Application.DTOs.Review;

namespace MinimalAirbnb.Application.Validators.Review;

/// <summary>
/// Yorum Ekleme DTO Validatörü
/// </summary>
public class AddReviewDtoValidator : AbstractValidator<AddReviewDto>
{
    public AddReviewDtoValidator()
    {
        RuleFor(x => x.GuestId)
            .NotEmpty().WithMessage("Misafir ID boş olamaz.");

        RuleFor(x => x.PropertyId)
            .NotEmpty().WithMessage("Ev ID boş olamaz.");

        RuleFor(x => x.ReservationId)
            .NotEmpty().WithMessage("Rezervasyon ID boş olamaz.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Puan 1-5 arasında olmalıdır.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Yorum başlığı boş olamaz.")
            .Length(5, 200).WithMessage("Yorum başlığı 5-200 karakter arasında olmalıdır.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Yorum içeriği boş olamaz.")
            .Length(10, 1000).WithMessage("Yorum içeriği 10-1000 karakter arasında olmalıdır.");

        RuleFor(x => x.CleanlinessRating)
            .InclusiveBetween(1, 5).WithMessage("Temizlik puanı 1-5 arasında olmalıdır.");

        RuleFor(x => x.CommunicationRating)
            .InclusiveBetween(1, 5).WithMessage("İletişim puanı 1-5 arasında olmalıdır.");

        RuleFor(x => x.CheckInRating)
            .InclusiveBetween(1, 5).WithMessage("Check-in puanı 1-5 arasında olmalıdır.");

        RuleFor(x => x.AccuracyRating)
            .InclusiveBetween(1, 5).WithMessage("Doğruluk puanı 1-5 arasında olmalıdır.");

        RuleFor(x => x.LocationRating)
            .InclusiveBetween(1, 5).WithMessage("Konum puanı 1-5 arasında olmalıdır.");

        RuleFor(x => x.ValueRating)
            .InclusiveBetween(1, 5).WithMessage("Değer puanı 1-5 arasında olmalıdır.");
    }
} 