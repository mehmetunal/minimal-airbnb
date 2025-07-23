using MediatR;
using MinimalAirbnb.Application.PropertyPhoto.DTOs;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.PropertyPhoto.Queries.GetPropertyPhotoById;

/// <summary>
/// PropertyPhoto by ID query
/// </summary>
public class GetPropertyPhotoByIdQuery : IRequest<Result<PropertyPhotoDto>>
{
    /// <summary>
    /// Ev fotoğrafı ID
    /// </summary>
    public Guid Id { get; set; }
} 