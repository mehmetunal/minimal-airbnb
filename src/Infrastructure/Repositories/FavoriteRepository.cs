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

    public async Task<Favorite> AddAsync(Favorite favorite)
    {
        await _context.Favorites.AddAsync(favorite);
        return favorite;
    }

    public async Task DeleteAsync(Guid id)
    {
        var favorite = await _context.Favorites.FindAsync(id);
        if (favorite != null)
        {
            _context.Favorites.Remove(favorite);
        }
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

    public async Task<IEnumerable<Favorite>> GetByPropertyIdAsync(Guid propertyId)
    {
        return await _context.Favorites
            .Include(f => f.User)
            .Include(f => f.Property)
            .Include(f => f.Property.Host)
            .Include(f => f.Property.Photos)
            .Where(f => f.PropertyId == propertyId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetFavoritePropertiesAsync(Guid userId)
    {
        return await _context.Favorites
            .Include(f => f.Property)
            .Include(f => f.Property.Host)
            .Include(f => f.Property.Photos)
            .Include(f => f.Property.Reviews)
            .Where(f => f.UserId == userId)
            .Select(f => f.Property)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(Guid userId, Guid propertyId)
    {
        return await _context.Favorites
            .AnyAsync(f => f.UserId == userId && f.PropertyId == propertyId);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
} 