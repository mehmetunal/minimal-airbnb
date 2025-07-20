using MediatR;

namespace MinimalAirbnb.Application.Commands.PropertyPhoto;

/// <summary>
/// Ev Fotoğrafı Silme Komutu
/// </summary>
public class DeletePropertyPhotoCommand : IRequest<bool>
{
    /// <summary>
    /// Ev fotoğrafı ID
    /// </summary>
    public Guid Id { get; set; }
} 