using FluentValidation;
using MinimalAirbnb.Application.DTOs.Message;

namespace MinimalAirbnb.Application.Validators.Message;

/// <summary>
/// Mesaj Güncelleme DTO Validatörü
/// </summary>
public class UpdateMessageDtoValidator : AbstractValidator<UpdateMessageDto>
{
    public UpdateMessageDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Mesaj ID boş olamaz.");

        RuleFor(x => x.Subject)
            .Length(5, 200).WithMessage("Konu 5-200 karakter arasında olmalıdır.");

        RuleFor(x => x.Content)
            .Length(10, 2000).WithMessage("Mesaj içeriği 10-2000 karakter arasında olmalıdır.");
    }
} 