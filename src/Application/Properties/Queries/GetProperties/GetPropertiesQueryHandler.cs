using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Properties.DTOs;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using MinimalAirbnb.Domain.Enums;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Properties.Queries.GetProperties;

/// <summary>
/// Properties listesi query handler'ı
/// </summary>
public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, Result<PagedList<PropertyDto>>>
{
    private readonly IPropertyRepository _propertyRepository;

    public GetPropertiesQueryHandler(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result<PagedList<PropertyDto>>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _propertyRepository.GetAll();

            // Filters
            if (!string.IsNullOrWhiteSpace(request.City))
            {
                query = query.Where(p => p.City.Contains(request.City));
            }

            if (request.MinPrice.HasValue)
            {
                query = query.Where(p => p.PricePerNight >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(p => p.PricePerNight <= request.MaxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.PropertyType))
            {
                if (Enum.TryParse<PropertyType>(request.PropertyType, out var propertyType))
                {
                    query = query.Where(p => p.PropertyType == propertyType);
                }
            }

            // Order by creation date
            query = query.OrderByDescending(p => p.CreatedDate);

            // Get paged list
            var pagedList = await query.ToPagedListAsync(request.PageNumber - 1, request.PageSize, new List<Filter>());

            // Map to DTOs
            var propertyDtos = pagedList.Data.Select(p => new PropertyDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                PropertyType = p.PropertyType,
                PricePerNight = p.PricePerNight,
                Address = p.Address,
                City = p.City,
                Country = p.Country,
                PostalCode = p.PostalCode,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                BedroomCount = p.BedroomCount,
                BathroomCount = p.BathroomCount,
                MaxGuestCount = p.MaxGuestCount,
                MinimumStayDays = p.MinimumStayDays,
                MaximumStayDays = p.MaximumStayDays,
                AverageRating = p.AverageRating,
                CreatedDate = p.CreatedDate,
                CreatedAt = p.CreatedDate
            }).ToList();

            // Create new PagedList with DTOs
            var resultPagedList = new PagedList<PropertyDto>(
                propertyDtos,
                request.PageNumber - 1,
                request.PageSize,
                pagedList.TotalCount
            );
            
            return Result<PagedList<PropertyDto>>.Success(resultPagedList, new SuccessMessage("200", "Properties başarıyla getirildi."));
        }
        catch (Exception ex)
        {
            return Result<PagedList<PropertyDto>>.Failure(new Error("500", $"Properties getirilirken hata oluştu: {ex.Message}"));
        }
    }
}
