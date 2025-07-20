namespace MinimalAirbnb.Application.DTOs.Favorite;

/// <summary>
/// Favori Güncelleme DTO
/// </summary>
public class UpdateFavoriteDto
{
    /// <summary>
    /// Favori ID
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Kullanıcı ID
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Ev ID
    /// </summary>
    public Guid PropertyId { get; set; }
} 