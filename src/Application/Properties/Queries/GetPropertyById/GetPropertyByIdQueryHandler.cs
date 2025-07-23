using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Properties.DTOs;
using Maggsoft.Core.Base;
using AutoMapper;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Properties.Queries.GetPropertyById;

/// <summary>
/// Property detayı için query handler
/// </summary>
public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, Result<PropertyDto>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;

    public GetPropertyByIdQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
    {
        _propertyRepository = propertyRepository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyDto>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Property'yi getir
            var property = await _propertyRepository.GetByIdAsync(request.Id);
            if (property == null)
                return Result<PropertyDto>.Failure(new Error("404", "Property bulunamadı."));

            // DTO'ya dönüştür
            var propertyDto = _mapper.Map<PropertyDto>(property);

            return Result<PropertyDto>.Success(propertyDto);
        }
        catch (Exception ex)
        {
            return Result<PropertyDto>.Failure(new Error("500", $"Property getirilirken hata oluştu: {ex.Message}"));
        }
    }
} 