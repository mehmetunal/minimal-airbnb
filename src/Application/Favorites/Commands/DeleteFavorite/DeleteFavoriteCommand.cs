using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Favorites.Commands.DeleteFavorite;

/// <summary>
/// Favorite silme command'i
/// </summary>
public class DeleteFavoriteCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
} 