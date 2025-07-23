using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Application.Properties.DTOs;

namespace MinimalAirbnb.Application.Properties.Queries.GetPropertyById;

/// <summary>
/// Property detayı için query
/// </summary>
public class GetPropertyByIdQuery : IRequest<Result<PropertyDto>>
{
    public Guid Id { get; set; }
} 