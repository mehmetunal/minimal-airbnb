using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Properties.Queries.GetPropertiesByHostId;
using MinimalAirbnb.Application.Properties.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using MinimalAirbnb.Application.Queries.Property;

namespace MinimalAirbnb.Application.Properties.Queries.GetPropertiesByHostId;

/// <summary>
/// Properties by Host ID query handler'ı
/// </summary>
public class GetPropertiesByHostIdQueryHandler : IRequestHandler<GetPropertiesByHostIdQuery, Result<List<PropertyDto>>>
{
    private readonly IPropertyRepository _propertyRepository;

    public GetPropertiesByHostIdQueryHandler(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result<List<PropertyDto>>> Handle(GetPropertiesByHostIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var properties = await _propertyRepository.GetByHostIdAsync(request.HostId);
            
            if (!properties.Any())
            {
                return Result<List<PropertyDto>>.Success(new List<PropertyDto>(), new SuccessMessage("200", "Bu ev sahibine ait mülk bulunamadı."));
            }

            var propertyDtos = properties.Select(p => new PropertyDto
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
                CreatedDate = p.CreatedDate
            }).ToList();

            return Result<List<PropertyDto>>.Success(propertyDtos, new SuccessMessage("200", "Ev sahibine ait mülkler başarıyla getirildi."));
        }
        catch (Exception ex)
        {
            return Result<List<PropertyDto>>.Failure(new Error("500", $"Mülkler getirilirken hata oluştu: {ex.Message}"));
        }
    }
} 