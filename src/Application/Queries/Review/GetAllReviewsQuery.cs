using MediatR;
using MinimalAirbnb.Application.DTOs.Review;

namespace MinimalAirbnb.Application.Queries.Review;

/// <summary>
/// Tüm Yorumları Getirme Sorgusu
/// </summary>
public class GetAllReviewsQuery : IRequest<IEnumerable<ReviewListDto>>
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
    /// Ev ID filtresi
    /// </summary>
    public Guid? PropertyId { get; set; }
    
    /// <summary>
    /// Misafir ID filtresi
    /// </summary>
    public Guid? GuestId { get; set; }
    
    /// <summary>
    /// Puan filtresi
    /// </summary>
    public int? Rating { get; set; }
} 