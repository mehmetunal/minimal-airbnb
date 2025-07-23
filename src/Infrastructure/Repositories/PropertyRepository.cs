using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Property Repository Implementation
/// </summary>
public class PropertyRepository : IPropertyRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public PropertyRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tüm property'leri getir (IQueryable)
    /// </summary>
    public IQueryable<Property> GetAll()
    {
        return _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Photos)
            .Include(p => p.Reviews)
            .Include(p => p.Reservations);
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Photos)
            .Include(p => p.Reviews)
            .Include(p => p.Reservations)
            .ToListAsync();
    }

    public async Task<Property?> GetByIdAsync(Guid id)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Photos)
            .Include(p => p.Reviews)
            .Include(p => p.Reservations)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Property> AddAsync(Property property)
    {
        await _context.Properties.AddAsync(property);
        return property;
    }

    public async Task<Property> UpdateAsync(Property property)
    {
        _context.Properties.Update(property);
        return property;
    }

    public async Task DeleteAsync(Guid id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property != null)
        {
            _context.Properties.Remove(property);
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Property>> GetByHostIdAsync(Guid hostId)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Photos)
            .Where(p => p.HostId == hostId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetByCityAsync(string city)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Photos)
            .Where(p => p.City.Contains(city))
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetByPropertyTypeAsync(PropertyType propertyType)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Photos)
            .Where(p => p.PropertyType == propertyType)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Photos)
            .Where(p => p.PricePerNight >= minPrice && p.PricePerNight <= maxPrice)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetByGuestCountAsync(int guestCount)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Photos)
            .Where(p => p.MaxGuestCount >= guestCount)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetPublishedPropertiesAsync()
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Photos)
            .Where(p => p.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> SearchPropertiesAsync(string? city = null, PropertyType? propertyType = null, 
        decimal? minPrice = null, decimal? maxPrice = null, int? guestCount = null, bool? hasWifi = null, 
        bool? hasAirConditioning = null, bool? hasKitchen = null, bool? hasParking = null, bool? hasPool = null, 
        bool? allowsPets = null, bool? allowsSmoking = null)
    {
        var query = _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Photos)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(city))
            query = query.Where(p => p.City.Contains(city));

        if (propertyType.HasValue)
            query = query.Where(p => p.PropertyType == propertyType.Value);

        if (minPrice.HasValue)
            query = query.Where(p => p.PricePerNight >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.PricePerNight <= maxPrice.Value);

        if (guestCount.HasValue)
            query = query.Where(p => p.MaxGuestCount >= guestCount.Value);

        if (hasWifi.HasValue)
            query = query.Where(p => p.HasWifi == hasWifi.Value);

        if (hasAirConditioning.HasValue)
            query = query.Where(p => p.HasAirConditioning == hasAirConditioning.Value);

        if (hasKitchen.HasValue)
            query = query.Where(p => p.HasKitchen == hasKitchen.Value);

        if (hasParking.HasValue)
            query = query.Where(p => p.HasParking == hasParking.Value);

        if (hasPool.HasValue)
            query = query.Where(p => p.HasPool == hasPool.Value);

        if (allowsPets.HasValue)
            query = query.Where(p => p.AllowsPets == allowsPets.Value);

        if (allowsSmoking.HasValue)
            query = query.Where(p => p.AllowsSmoking == allowsSmoking.Value);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetAvailablePropertiesAsync(DateTime checkInDate, DateTime checkOutDate, int guestCount)
    {
        // Bu metod daha karmaşık bir implementasyon gerektirir
        // Şimdilik basit bir implementasyon
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Photos)
            .Where(p => p.MaxGuestCount >= guestCount && p.IsActive)
            .ToListAsync();
    }
} 