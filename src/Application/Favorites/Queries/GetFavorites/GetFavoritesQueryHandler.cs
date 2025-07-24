using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Favorites.DTOs;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Extensions;
using Microsoft.EntityFrameworkCore;

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

            // Map to DTOs with property information
            var favoriteDtos = pagedList.Data.Select(f => new FavoriteDto
            {
                Id = f.Id,
                UserId = f.UserId,
                PropertyId = f.PropertyId,
                CreatedDate = f.CreatedDate,
                CreatedAt = f.CreatedDate,
                UpdatedDate = f.ModifiedDate ?? f.CreatedDate,
                Note = f.Note,
                Category = f.Category,
                Tags = f.Tags?.Split(',').ToList() ?? new List<string>(),
                Priority = f.SortOrder,
                IsPrivate = f.IsPrivate,
                IsShared = f.IsShared,
                ShareLink = f.ShareLink,
                SharedDate = f.SharedDate,
                ViewCount = f.VisitCount,
                LikeCount = 0, // Not available in entity
                CommentCount = 0, // Not available in entity
                CopyCount = 0, // Not available in entity
                NotificationsEnabled = true, // Default value
                PriceChangeNotifications = true, // Default value
                AvailabilityNotifications = true, // Default value
                ReviewNotifications = true, // Default value
                
                // Property bilgileri
                PropertyTitle = f.Property?.Title ?? string.Empty,
                PropertyDescription = f.Property?.Description ?? string.Empty,
                PropertyPhotoUrl = null, // Not available in entity
                MainPhotoUrl = null, // Not available in entity
                PropertyPricePerNight = f.Property?.PricePerNight ?? 0,
                PricePerNight = f.Property?.PricePerNight ?? 0,
                PropertyAverageRating = f.Property?.AverageRating,
                AverageRating = f.Property?.AverageRating,
                PropertyTotalReviews = f.Property?.TotalReviews ?? 0,
                PropertyCity = f.Property?.City ?? string.Empty,
                City = f.Property?.City ?? string.Empty,
                PropertyCountry = f.Property?.Country ?? string.Empty,
                PropertyFullAddress = f.Property?.Address ?? string.Empty,
                PropertyType = f.Property?.PropertyType.ToString() ?? string.Empty,
                PropertyGuestCapacity = f.Property?.MaxGuestCount ?? 0,
                PropertyBedroomCount = f.Property?.BedroomCount ?? 0,
                PropertyBathroomCount = f.Property?.BathroomCount ?? 0,
                PropertyBedCount = f.Property?.BedCount ?? 0,
                PropertyAmenities = new List<string>(), // Not available in entity
                
                // Host bilgileri
                HostId = f.Property?.HostId ?? Guid.Empty,
                HostName = $"{f.Property?.Host?.FirstName} {f.Property?.Host?.LastName}".Trim(),
                HostPhotoUrl = f.Property?.Host?.ProfilePicture,
                HostIsSuperHost = false, // Not available in entity
                HostIsVerified = f.Property?.Host?.IsVerified ?? false,
                
                // User bilgileri
                UserName = f.User?.UserName ?? string.Empty,
                UserFullName = $"{f.User?.FirstName} {f.User?.LastName}".Trim(),
                UserPhotoUrl = f.User?.ProfilePicture
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
