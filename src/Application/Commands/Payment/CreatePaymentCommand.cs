using MediatR;
using MinimalAirbnb.Application.DTOs.Payment;

namespace MinimalAirbnb.Application.Commands.Payment;

/// <summary>
/// Ödeme Oluşturma Komutu
/// </summary>
public class CreatePaymentCommand : IRequest<PaymentResultDto>
{
    /// <summary>
    /// Ödeme ekleme DTO
    /// </summary>
    public AddPaymentDto AddPaymentDto { get; set; } = new();
} 