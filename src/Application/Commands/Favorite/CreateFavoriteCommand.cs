using MediatR;
using MinimalAirbnb.Application.DTOs.Favorite;

namespace MinimalAirbnb.Application.Commands.Favorite;

/// <summary>
/// Favori Oluşturma Komutu
/// </summary>
public class CreateFavoriteCommand : IRequest<FavoriteResultDto>
{
    /// <summary>
    /// Favori ekleme DTO
    /// </summary>
    public AddFavoriteDto AddFavoriteDto { get; set; } = new();
} 