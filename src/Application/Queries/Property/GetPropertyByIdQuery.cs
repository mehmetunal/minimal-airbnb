using MediatR;
using MinimalAirbnb.Application.DTOs.Property;

namespace MinimalAirbnb.Application.Queries.Property;

/// <summary>
/// ID'ye Göre Ev Getirme Sorgusu
/// </summary>
public class GetPropertyByIdQuery : IRequest<PropertyResultDto?>
{
    /// <summary>
    /// Ev ID
    /// </summary>
    public Guid Id { get; set; }
} 