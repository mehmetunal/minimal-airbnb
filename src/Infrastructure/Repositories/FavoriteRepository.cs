using Microsoft.EntityFrameworkCore;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Infrastructure.Data;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Favorite Repository Implementation
/// </summary>
public class FavoriteRepository : IFavoriteRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public FavoriteRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tüm favorite'ları getir (IQueryable)
    /// </summary>
    public IQueryable<Favorite> GetAll()
    {
        return _context.Favorites
            .Include(f => f.User)
            .Include(f => f.Property);
    }

    public async Task<IEnumerable<Favorite>> GetAllAsync()
    {
        return await _context.Favorites
            .Include(f => f.User)
            .Include(f => f.Property)
            .ToListAsync();
    }

    public async Task<Favorite?> GetByIdAsync(Guid id)
    {
        return await _context.Favorites
            .Include(f => f.User)
            .Include(f => f.Property)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    /// <summary>
    /// Kullanıcıya göre favorite'ları getir
    /// </summary>
    public async Task<IEnumerable<Favorite>> GetByUserAsync(Guid userId)
    {
        return await _context.Favorites
            .Include(f => f.User)
            .Include(f => f.Property)
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }

    /// <summary>
    /// Property'ye göre favorite'ları getir
    /// </summary>
    public async Task<IEnumerable<Favorite>> GetByPropertyAsync(Guid propertyId)
    {
        return await _context.Favorites
            .Include(f => f.User)
            .Include(f => f.Property)
            .Where(f => f.PropertyId == propertyId)
            .ToListAsync();
    }

    /// <summary>
    /// Kullanıcı ve property'ye göre favorite getir
    /// </summary>
    public async Task<Favorite?> GetByUserAndPropertyAsync(Guid userId, Guid propertyId)
    {
        return await _context.Favorites
            .Include(f => f.User)
            .Include(f => f.Property)
            .FirstOrDefaultAsync(f => f.UserId == userId && f.PropertyId == propertyId);
    }

    public async Task<Favorite> AddAsync(Favorite favorite)
    {
        await _context.Favorites.AddAsync(favorite);
        return favorite;
    }

    public async Task<Favorite> UpdateAsync(Favorite favorite)
    {
        _context.Favorites.Update(favorite);
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

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
} 