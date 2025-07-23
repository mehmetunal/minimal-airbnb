using MediatR;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Application.Favorites.DTOs;

namespace MinimalAirbnb.Application.Favorites.Queries.GetFavorites;

/// <summary>
/// Favorites listesi query'si
/// </summary>
public class GetFavoritesQuery : IRequest<PagedList<FavoriteDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? UserId { get; set; }
    public Guid? PropertyId { get; set; }
} 