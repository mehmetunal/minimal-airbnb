using MediatR;
using MinimalAirbnb.Application.DTOs.Payment;

namespace MinimalAirbnb.Application.Queries.Payment;

/// <summary>
/// ID'ye Göre Ödeme Getirme Sorgusu
/// </summary>
public class GetPaymentByIdQuery : IRequest<PaymentResultDto?>
{
    /// <summary>
    /// Ödeme ID
    /// </summary>
    public Guid Id { get; set; }
} 