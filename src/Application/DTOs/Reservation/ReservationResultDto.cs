using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.DTOs.Reservation;

/// <summary>
/// Rezervasyon Sonuç DTO
/// </summary>
public class ReservationResultDto
{
    /// <summary>
    /// Rezervasyon ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Misafir ID
    /// </summary>
    public Guid GuestId { get; set; }
    
    /// <summary>
    /// Misafir adı
    /// </summary>
    public string GuestName { get; set; } = string.Empty;
    
    /// <summary>
    /// Ev ID
    /// </summary>
    public Guid PropertyId { get; set; }
    
    /// <summary>
    /// Ev başlığı
    /// </summary>
    public string PropertyTitle { get; set; } = string.Empty;
    
    /// <summary>
    /// Ev sahibi ID
    /// </summary>
    public Guid HostId { get; set; }
    
    /// <summary>
    /// Ev sahibi adı
    /// </summary>
    public string HostName { get; set; } = string.Empty;
    
    /// <summary>
    /// Check-in tarihi
    /// </summary>
    public DateTime CheckInDate { get; set; }
    
    /// <summary>
    /// Check-out tarihi
    /// </summary>
    public DateTime CheckOutDate { get; set; }
    
    /// <summary>
    /// Misafir sayısı
    /// </summary>
    public int GuestCount { get; set; }
    
    /// <summary>
    /// Toplam fiyat
    /// </summary>
    public decimal TotalPrice { get; set; }
    
    /// <summary>
    /// Özel istekler
    /// </summary>
    public string? SpecialRequests { get; set; }
    
    /// <summary>
    /// Rezervasyon durumu
    /// </summary>
    public ReservationStatus Status { get; set; }
    
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