using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.DTOs.Payment;

/// <summary>
/// Ödeme Güncelleme DTO
/// </summary>
public class UpdatePaymentDto
{
    /// <summary>
    /// Ödeme ID
    /// </summary>
    public Guid Id { get; set; }
    
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
} 