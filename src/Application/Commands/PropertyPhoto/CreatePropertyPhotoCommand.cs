using MediatR;
using MinimalAirbnb.Application.DTOs.PropertyPhoto;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Commands.PropertyPhoto;

/// <summary>
/// Ev Fotoğrafı Oluşturma Komutu
/// </summary>
public class CreatePropertyPhotoCommand : IRequest<Result<PropertyPhotoResultDto>>
{
    /// <summary>
    /// Ev fotoğrafı ekleme DTO
    /// </summary>
    public AddPropertyPhotoDto AddPropertyPhotoDto { get; set; } = new();
} 