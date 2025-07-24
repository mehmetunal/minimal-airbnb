using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Payments.DTOs;

/// <summary>
/// Ödeme oluşturma response DTO'su
/// </summary>
public class CreatePaymentResponseDto
{
    public Guid PaymentId { get; set; }
    public Guid ReservationId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus Status { get; set; }
    public PaymentProvider Provider { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public string ProviderTransactionId { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; }
    public string Currency { get; set; } = "TRY";
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public string ErrorCode { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
} 