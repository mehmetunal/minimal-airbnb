using MediatR;
using MinimalAirbnb.Application.Common.Models;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Properties.DTOs;
using MinimalAirbnb.Domain.Entities;
using System.Linq.Expressions;

namespace MinimalAirbnb.Application.Properties.Queries.GetProperties;

/// <summary>
/// GetPropertiesQuery için handler
/// </summary>
public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, PaginatedApiResponse<PropertyDto>>
{
    private readonly IPropertyRepository _propertyRepository;

    public GetPropertiesQueryHandler(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<PaginatedApiResponse<PropertyDto>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Repository'den tüm property'leri al
            var properties = await _propertyRepository.GetPublishedPropertiesAsync();

            // Filtreleme
            if (!string.IsNullOrEmpty(request.City))
            {
                properties = properties.Where(p => p.City.Contains(request.City));
            }

            if (request.PropertyType.HasValue)
            {
                properties = properties.Where(p => p.PropertyType == request.PropertyType.Value);
            }

            if (request.MinPrice.HasValue)
            {
                properties = properties.Where(p => p.PricePerNight >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                properties = properties.Where(p => p.PricePerNight <= request.MaxPrice.Value);
            }

            if (request.GuestCount.HasValue)
            {
                properties = properties.Where(p => p.MaxGuestCount >= request.GuestCount.Value);
            }

            // Sıralama
            switch (request.SortBy?.ToLower())
            {
                case "price":
                    properties = request.SortOrder?.ToLower() == "desc" 
                        ? properties.OrderByDescending(p => p.PricePerNight)
                        : properties.OrderBy(p => p.PricePerNight);
                    break;
                case "rating":
                    properties = request.SortOrder?.ToLower() == "desc"
                        ? properties.OrderByDescending(p => p.AverageRating)
                        : properties.OrderBy(p => p.AverageRating);
                    break;
                case "createddate":
                    properties = request.SortOrder?.ToLower() == "desc"
                        ? properties.OrderByDescending(p => p.CreatedDate)
                        : properties.OrderBy(p => p.CreatedDate);
                    break;
                default:
                    properties = properties.OrderByDescending(p => p.CreatedDate);
                    break;
            }

            var totalCount = properties.Count();

            // Sayfalama
            var pagedProperties = properties
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            // DTO'lara dönüştür
            var propertyDtos = pagedProperties.Select(MapToDto).ToList();

            return new PaginatedApiResponse<PropertyDto>
            {
                Success = true,
                Message = "Properties başarıyla getirildi",
                Data = propertyDtos,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
            };
        }
        catch (Exception ex)
        {
            return new PaginatedApiResponse<PropertyDto>
            {
                Success = false,
                Message = $"Properties getirilirken hata oluştu: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// Property entity'sini DTO'ya dönüştürür
    /// </summary>
    private PropertyDto MapToDto(Property property)
    {
        return new PropertyDto
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            HostId = property.HostId,
            HostName = property.Host?.FirstName + " " + property.Host?.LastName,
            PropertyType = property.PropertyType,
            BedroomCount = property.BedroomCount,
            BedCount = property.BedCount,
            BathroomCount = property.BathroomCount,
            MaxGuestCount = property.MaxGuestCount,
            PricePerNight = property.PricePerNight,
            CleaningFee = property.CleaningFee,
            ServiceFee = property.ServiceFee,
            TotalPricePerNight = property.PricePerNight + property.CleaningFee + property.ServiceFee,
            Address = property.Address,
            City = property.City,
            Country = property.Country,
            PostalCode = property.PostalCode,
            FullAddress = $"{property.Address}, {property.City}, {property.Country}",
            Latitude = property.Latitude,
            Longitude = property.Longitude,
            HasWifi = property.HasWifi,
            HasAirConditioning = property.HasAirConditioning,
            HasKitchen = property.HasKitchen,
            HasParking = property.HasParking,
            HasPool = property.HasPool,
            AllowsPets = property.AllowsPets,
            AllowsSmoking = property.AllowsSmoking,
            MinimumStayDays = property.MinimumStayDays,
            MaximumStayDays = property.MaximumStayDays,
            CancellationPolicyDays = property.CancellationPolicyDays,
            AverageRating = property.AverageRating,
            TotalReviews = property.Reviews?.Count ?? 0,
            ViewCount = property.ViewCount,
            FavoriteCount = property.Favorites?.Count ?? 0,
            ReservationCount = property.Reservations?.Count ?? 0,
            MainPhotoUrl = property.Photos?.FirstOrDefault()?.PhotoUrl,
            PhotoCount = property.Photos?.Count ?? 0,
            CreatedDate = property.CreatedDate,
            IsAvailable = true // Müsaitlik kontrolü CheckInDate ve CheckOutDate ile yapılacak
        };
    }
} 