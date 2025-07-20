using FluentValidation;
using MinimalAirbnb.Application.DTOs.Favorite;

namespace MinimalAirbnb.Application.Validators.Favorite;

/// <summary>
/// Favori Ekleme DTO Validatörü
/// </summary>
public class AddFavoriteDtoValidator : AbstractValidator<AddFavoriteDto>
{
    public AddFavoriteDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");

        RuleFor(x => x.PropertyId)
            .NotEmpty().WithMessage("Ev ID boş olamaz.");


    }
} 