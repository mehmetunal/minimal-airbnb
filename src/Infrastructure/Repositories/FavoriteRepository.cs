using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Favori Repository Implementasyonu
/// </summary>
public class FavoriteRepository : IFavoriteRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public FavoriteRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Favorite>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Favorites
            .Include(f => f.User)
            .Include(f => f.Property)
            .Include(f => f.Property.Host)
            .Include(f => f.Property.Photos)
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }

    public async Task<int> GetFavoriteCountByPropertyIdAsync(Guid propertyId)
    {
        return await _context.Favorites
            .CountAsync(f => f.PropertyId == propertyId);
    }

    public async Task<bool> IsFavoriteAsync(Guid userId, Guid propertyId)
    {
        return await _context.Favorites
            .AnyAsync(f => f.UserId == userId && f.PropertyId == propertyId);
    }

    public async Task<bool> ToggleFavoriteAsync(Guid userId, Guid propertyId)
    {
        var existingFavorite = await _context.Favorites
            .FirstOrDefaultAsync(f => f.UserId == userId && f.PropertyId == propertyId);

        if (existingFavorite != null)
        {
            // Favori varsa kaldır
            _context.Favorites.Remove(existingFavorite);
            return false; // Artık favori değil
        }
        else
        {
            // Favori yoksa ekle
            var newFavorite = new Favorite
            {
                UserId = userId,
                PropertyId = propertyId,
                CreatedDate = DateTime.UtcNow
            };
            
            await _context.Favorites.AddAsync(newFavorite);
            return true; // Artık favori
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
} 