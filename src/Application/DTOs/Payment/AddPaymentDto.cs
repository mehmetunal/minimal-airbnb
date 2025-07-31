using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.DTOs.Payment;

/// <summary>
/// Ödeme Ekleme DTO
/// </summary>
public class AddPaymentDto
{
    /// <summary>
    /// Rezervasyon ID
    /// </summary>
    public Guid ReservationId { get; set; }
    
    /// <summary>
    /// Kullanıcı ID
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Misafir ID
    /// </summary>
    public Guid GuestId { get; set; }
    
    /// <summary>
    /// Ödeme tutarı
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Para birimi
    /// </summary>
    public string Currency { get; set; } = "TRY";
    
    /// <summary>
    /// Ödeme yöntemi
    /// </summary>
    public PaymentMethod PaymentMethod { get; set; }
    
    /// <summary>
    /// Ödeme durumu
    /// </summary>
    public PaymentStatus Status { get; set; }

    /// <summary>
    /// İşlem ID'si
    /// </summary>
    public string? TransactionId { get; set; }
    
    /// <summary>
    /// Ödeme tarihi
    /// </summary>
    public DateTime PaymentDate { get; set; }
    
    /// <summary>
    /// Ödeme açıklaması
    /// </summary>
    public string? Description { get; set; }
} 