using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Application.Payments.DTOs;

namespace MinimalAirbnb.Application.Payments.Commands.CreatePayment;

/// <summary>
/// Payment oluşturma command'i
/// </summary>
public class CreatePaymentCommand : IRequest<Result<CreatePaymentResponseDto>>
{
    public Guid ReservationId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "TRY";
    public string PaymentMethod { get; set; } = string.Empty;
    public string? CardNumber { get; set; }
    public string? CardHolderName { get; set; }
    public string? ExpiryDate { get; set; }
    public string? CVV { get; set; }
    public string? PayPalEmail { get; set; }
    public string? BankAccountNumber { get; set; }
    public string? Description { get; set; }
    public string? CallbackUrl { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
} 