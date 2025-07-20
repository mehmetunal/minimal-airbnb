using MediatR;
using MinimalAirbnb.Application.DTOs.Favorite;

namespace MinimalAirbnb.Application.Queries.Favorite;

/// <summary>
/// ID'ye Göre Favori Getirme Sorgusu
/// </summary>
public class GetFavoriteByIdQuery : IRequest<FavoriteResultDto?>
{
    /// <summary>
    /// Favori ID
    /// </summary>
    public Guid Id { get; set; }
} 