using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.DTOs.Payment;

/// <summary>
/// Ödeme Ekleme DTO
/// </summary>
public class AddPaymentDto
{
    /// <summary>
    /// Kullanıcı ID
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Rezervasyon ID
    /// </summary>
    public Guid ReservationId { get; set; }
    
    /// <summary>
    /// Ödeme tutarı
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Ödeme yöntemi
    /// </summary>
    public string PaymentMethod { get; set; } = string.Empty;
    
    /// <summary>
    /// Ödeme durumu
    /// </summary>
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    
    /// <summary>
    /// İşlem ID
    /// </summary>
    public string? TransactionId { get; set; }
    
    /// <summary>
    /// Ödeme tarihi
    /// </summary>
    public DateTime PaymentDate { get; set; }
    
    /// <summary>
    /// Açıklama
    /// </summary>
    public string? Description { get; set; }
} 