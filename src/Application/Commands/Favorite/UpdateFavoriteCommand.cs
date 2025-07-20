using MediatR;
using MinimalAirbnb.Application.DTOs.Favorite;

namespace MinimalAirbnb.Application.Commands.Favorite;

/// <summary>
/// Favori Güncelleme Komutu
/// </summary>
public class UpdateFavoriteCommand : IRequest<FavoriteResultDto>
{
    /// <summary>
    /// Favori güncelleme DTO
    /// </summary>
    public UpdateFavoriteDto UpdateFavoriteDto { get; set; } = new();
} 