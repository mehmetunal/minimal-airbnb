using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Favorites.DTOs;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Extensions;

namespace MinimalAirbnb.Application.Favorites.Queries.GetFavorites;

/// <summary>
/// Favorites listesi query handler'ı
/// </summary>
public class GetFavoritesQueryHandler : IRequestHandler<GetFavoritesQuery, PagedList<FavoriteDto>>
{
    private readonly IFavoriteRepository _favoriteRepository;

    public GetFavoritesQueryHandler(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    public async Task<PagedList<FavoriteDto>> Handle(GetFavoritesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _favoriteRepository.GetAll();

            // Filters
            if (request.UserId.HasValue)
            {
                query = query.Where(f => f.UserId == request.UserId.Value);
            }

            if (request.PropertyId.HasValue)
            {
                query = query.Where(f => f.PropertyId == request.PropertyId.Value);
            }

            // Order by creation date
            query = query.OrderByDescending(f => f.CreatedDate);

            // Get paged list
            var pagedList = await query.ToPagedListAsync(request.PageNumber - 1, request.PageSize);

            // Map to DTOs
            var favoriteDtos = pagedList.Data.Select(f => new FavoriteDto
            {
                Id = f.Id,
                UserId = f.UserId,
                PropertyId = f.PropertyId
            }).ToList();

            // Create new PagedList with DTOs
            return new PagedList<FavoriteDto>(
                favoriteDtos,
                request.PageNumber - 1,
                request.PageSize,
                pagedList.TotalCount
            );
        }
        catch (Exception ex)
        {
            return new PagedList<FavoriteDto>(
                new List<FavoriteDto>(),
                request.PageNumber - 1,
                request.PageSize,
                0
            );
        }
    }
}
