using AutoMapper;
using MediatR;
using MinimalAirbnb.Application.Common.Models;
using MinimalAirbnb.Application.Properties.DTOs;
using MinimalAirbnb.Application.Interfaces;

namespace MinimalAirbnb.Application.Properties.Queries.GetPropertyById;

/// <summary>
/// GetPropertyByIdQuery için handler
/// </summary>
public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, ApiResponse<PropertyDto>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;

    public GetPropertyByIdQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
    {
        _propertyRepository = propertyRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PropertyDto>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Property'yi getir
            var properties = await _propertyRepository.GetPublishedPropertiesAsync();
            var property = properties.FirstOrDefault(p => p.Id == request.Id);

            if (property == null)
            {
                return new ApiResponse<PropertyDto>
                {
                    Success = false,
                    Message = "Ev bulunamadı"
                };
            }

            // Property'yi DTO'ya dönüştür
            var propertyDto = _mapper.Map<PropertyDto>(property);

            return new ApiResponse<PropertyDto>
            {
                Success = true,
                Message = "Ev başarıyla getirildi",
                Data = propertyDto
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<PropertyDto>
            {
                Success = false,
                Message = $"Ev getirilirken hata oluştu: {ex.Message}"
            };
        }
    }
} 