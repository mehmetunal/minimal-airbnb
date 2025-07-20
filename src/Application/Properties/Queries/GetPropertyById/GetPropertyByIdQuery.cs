using MediatR;
using MinimalAirbnb.Application.Common.Models;
using MinimalAirbnb.Application.Properties.DTOs;

namespace MinimalAirbnb.Application.Properties.Queries.GetPropertyById;

/// <summary>
/// Property ID'ye göre getirme query'si
/// </summary>
public class GetPropertyByIdQuery : IRequest<ApiResponse<PropertyDto>>
{
    /// <summary>
    /// Property ID'si
    /// </summary>
    public Guid Id { get; set; }
} 