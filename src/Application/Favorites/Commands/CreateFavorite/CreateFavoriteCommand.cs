using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Favorites.Commands.CreateFavorite;

/// <summary>
/// Favorite oluşturma command'i
/// </summary>
public class CreateFavoriteCommand : IRequest<Result<object>>
{
    public Guid UserId { get; set; }
    public Guid PropertyId { get; set; }
} 