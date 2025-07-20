using FluentValidation;
using MinimalAirbnb.Application.DTOs.Message;

namespace MinimalAirbnb.Application.Validators.Message;

/// <summary>
/// Mesaj Ekleme DTO Validatörü
/// </summary>
public class AddMessageDtoValidator : AbstractValidator<AddMessageDto>
{
    public AddMessageDtoValidator()
    {
        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("Gönderen ID boş olamaz.");

        RuleFor(x => x.ReceiverId)
            .NotEmpty().WithMessage("Alıcı ID boş olamaz.");

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Konu boş olamaz.")
            .Length(5, 200).WithMessage("Konu 5-200 karakter arasında olmalıdır.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Mesaj içeriği boş olamaz.")
            .Length(10, 2000).WithMessage("Mesaj içeriği 10-2000 karakter arasında olmalıdır.");

        RuleFor(x => x.ReservationId)
            .NotEmpty().WithMessage("Rezervasyon ID boş olamaz.")
            .When(x => x.ReservationId.HasValue);

        RuleFor(x => x.PropertyId)
            .NotEmpty().WithMessage("Ev ID boş olamaz.")
            .When(x => x.PropertyId.HasValue);
    }
} 