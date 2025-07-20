using MediatR;
using MinimalAirbnb.Application.DTOs.Payment;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Queries.Payment;

/// <summary>
/// Tüm Ödemeleri Getirme Sorgusu
/// </summary>
public class GetAllPaymentsQuery : IRequest<IEnumerable<PaymentListDto>>
{
    /// <summary>
    /// Sayfa numarası
    /// </summary>
    public int PageNumber { get; set; } = 1;
    
    /// <summary>
    /// Sayfa boyutu
    /// </summary>
    public int PageSize { get; set; } = 10;
    
    /// <summary>
    /// Kullanıcı ID filtresi
    /// </summary>
    public Guid? UserId { get; set; }
    
    /// <summary>
    /// Rezervasyon ID filtresi
    /// </summary>
    public Guid? ReservationId { get; set; }
    
    /// <summary>
    /// Durum filtresi
    /// </summary>
    public PaymentStatus? Status { get; set; }
    
    /// <summary>
    /// Başlangıç tarihi
    /// </summary>
    public DateTime? StartDate { get; set; }
    
    /// <summary>
    /// Bitiş tarihi
    /// </summary>
    public DateTime? EndDate { get; set; }
} 