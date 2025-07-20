using MediatR;

namespace MinimalAirbnb.Application.Commands.Favorite;

/// <summary>
/// Favori Silme Komutu
/// </summary>
public class DeleteFavoriteCommand : IRequest<bool>
{
    /// <summary>
    /// Favori ID
    /// </summary>
    public Guid Id { get; set; }
} 