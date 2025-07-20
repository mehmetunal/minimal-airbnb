using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.DTOs.Payment;

/// <summary>
/// Ödeme Sonuç DTO
/// </summary>
public class PaymentResultDto
{
    /// <summary>
    /// Ödeme ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Kullanıcı ID
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Kullanıcı adı
    /// </summary>
    public string UserName { get; set; } = string.Empty;
    
    /// <summary>
    /// Rezervasyon ID
    /// </summary>
    public Guid ReservationId { get; set; }
    
    /// <summary>
    /// Ev başlığı
    /// </summary>
    public string PropertyTitle { get; set; } = string.Empty;
    
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
    public PaymentStatus Status { get; set; }
    
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
    
    /// <summary>
    /// İptal nedeni
    /// </summary>
    public string? CancellationReason { get; set; }
    
    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Güncellenme tarihi
    /// </summary>
    public DateTime UpdatedAt { get; set; }
} 