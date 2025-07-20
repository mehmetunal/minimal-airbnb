using FluentValidation;
using MinimalAirbnb.Application.DTOs.Favorite;

namespace MinimalAirbnb.Application.Validators.Favorite;

/// <summary>
/// Favori Güncelleme DTO Validatörü
/// </summary>
public class UpdateFavoriteDtoValidator : AbstractValidator<UpdateFavoriteDto>
{
    public UpdateFavoriteDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Favori ID boş olamaz.");
    }
} 