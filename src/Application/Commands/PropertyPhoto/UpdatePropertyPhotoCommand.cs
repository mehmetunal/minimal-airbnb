using MediatR;
using MinimalAirbnb.Application.DTOs.PropertyPhoto;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Commands.PropertyPhoto;

/// <summary>
/// Ev Fotoğrafı Güncelleme Komutu
/// </summary>
public class UpdatePropertyPhotoCommand : IRequest<Result<PropertyPhotoResultDto>>
{
    /// <summary>
    /// Ev fotoğrafı ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Ev fotoğrafı güncelleme DTO
    /// </summary>
    public UpdatePropertyPhotoDto UpdatePropertyPhotoDto { get; set; } = new();
} 