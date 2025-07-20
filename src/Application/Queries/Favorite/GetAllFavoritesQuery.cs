using MediatR;
using MinimalAirbnb.Application.DTOs.Favorite;

namespace MinimalAirbnb.Application.Queries.Favorite;

/// <summary>
/// Tüm Favorileri Getirme Sorgusu
/// </summary>
public class GetAllFavoritesQuery : IRequest<IEnumerable<FavoriteListDto>>
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
    /// Ev ID filtresi
    /// </summary>
    public Guid? PropertyId { get; set; }
} 