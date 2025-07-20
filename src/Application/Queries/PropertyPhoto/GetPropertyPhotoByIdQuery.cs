using MediatR;
using MinimalAirbnb.Application.DTOs.PropertyPhoto;

namespace MinimalAirbnb.Application.Queries.PropertyPhoto;

/// <summary>
/// ID'ye Göre Ev Fotoğrafı Getirme Sorgusu
/// </summary>
public class GetPropertyPhotoByIdQuery : IRequest<PropertyPhotoResultDto?>
{
    /// <summary>
    /// Ev fotoğrafı ID
    /// </summary>
    public Guid Id { get; set; }
} 