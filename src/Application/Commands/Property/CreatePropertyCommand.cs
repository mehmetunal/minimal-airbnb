using MediatR;
using MinimalAirbnb.Application.DTOs.Property;

namespace MinimalAirbnb.Application.Commands.Property;

/// <summary>
/// Ev Oluşturma Komutu
/// </summary>
public class CreatePropertyCommand : IRequest<PropertyResultDto>
{
    /// <summary>
    /// Ev ekleme DTO
    /// </summary>
    public AddPropertyDto AddPropertyDto { get; set; } = new();
} 