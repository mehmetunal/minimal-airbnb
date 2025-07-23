using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.PropertyPhoto.Commands.DeletePropertyPhoto;

/// <summary>
/// PropertyPhoto silme command
/// </summary>
public class DeletePropertyPhotoCommand : IRequest<Result<object>>
{
    /// <summary>
    /// Ev fotoğrafı ID
    /// </summary>
    public Guid Id { get; set; }
} 