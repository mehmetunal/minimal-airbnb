namespace MinimalAirbnb.Application.DTOs.Favorite;

/// <summary>
/// Favori Ekleme DTO
/// </summary>
public class AddFavoriteDto
{
    /// <summary>
    /// Kullanıcı ID
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Ev ID
    /// </summary>
    public Guid PropertyId { get; set; }
} 