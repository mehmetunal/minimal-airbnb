using MediatR;
using MinimalAirbnb.Application.DTOs.Reservation;
using MinimalAirbnb.Domain.Enums;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Commands.Reservation;

/// <summary>
/// Rezervasyon Oluşturma Komutu
/// </summary>
public class CreateReservationCommand : IRequest<Result<ReservationResultDto>>
{
    /// <summary>
    /// Misafir ID
    /// </summary>
    public Guid GuestId { get; set; }

    /// <summary>
    /// Kullanıcı ID (alias)
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Ev ID
    /// </summary>
    public Guid PropertyId { get; set; }
    
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
    /// Gecelik fiyat
    /// </summary>
    public decimal PricePerNight { get; set; }

    /// <summary>
    /// Temizlik ücreti
    /// </summary>
    public decimal CleaningFee { get; set; }

    /// <summary>
    /// Hizmet ücreti
    /// </summary>
    public decimal ServiceFee { get; set; }

    /// <summary>
    /// Toplam fiyat
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Rezervasyon durumu
    /// </summary>
    public ReservationStatus Status { get; set; }

    /// <summary>
    /// Check-in saati
    /// </summary>
    public TimeSpan CheckInTime { get; set; }

    /// <summary>
    /// Check-out saati
    /// </summary>
    public TimeSpan CheckOutTime { get; set; }

    /// <summary>
    /// Özel istekler
    /// </summary>
    public string? SpecialRequests { get; set; }

    /// <summary>
    /// Notlar (alias)
    /// </summary>
    public string? Notes { get; set; }
} 