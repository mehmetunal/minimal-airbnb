using MediatR;
using MinimalAirbnb.Application.DTOs.PropertyPhoto;

namespace MinimalAirbnb.Application.Commands.PropertyPhoto;

/// <summary>
/// Ev Fotoğrafı Güncelleme Komutu
/// </summary>
public class UpdatePropertyPhotoCommand : IRequest<PropertyPhotoResultDto>
{
    /// <summary>
    /// Ev fotoğrafı güncelleme DTO
    /// </summary>
    public UpdatePropertyPhotoDto UpdatePropertyPhotoDto { get; set; } = new();
} 