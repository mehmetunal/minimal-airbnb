using MediatR;
using MinimalAirbnb.Application.DTOs.Payment;

namespace MinimalAirbnb.Application.Commands.Payment;

/// <summary>
/// Ödeme Güncelleme Komutu
/// </summary>
public class UpdatePaymentCommand : IRequest<PaymentResultDto>
{
    /// <summary>
    /// Ödeme güncelleme DTO
    /// </summary>
    public UpdatePaymentDto UpdatePaymentDto { get; set; } = new();
} 