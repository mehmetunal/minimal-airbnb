using MediatR;
using MinimalAirbnb.Application.DTOs.Property;

namespace MinimalAirbnb.Application.Commands.Property;

/// <summary>
/// Ev Güncelleme Komutu
/// </summary>
public class UpdatePropertyCommand : IRequest<PropertyResultDto>
{
    /// <summary>
    /// Ev güncelleme DTO
    /// </summary>
    public UpdatePropertyDto UpdatePropertyDto { get; set; } = new();
} 