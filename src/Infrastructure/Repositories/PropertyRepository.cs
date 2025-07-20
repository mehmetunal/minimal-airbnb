using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Ev Repository Implementasyonu
/// </summary>
public class PropertyRepository : IPropertyRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public PropertyRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Property>> GetByHostIdAsync(Guid hostId)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Reservations)
            .Include(p => p.Reviews)
            .Include(p => p.Photos)
            .Include(p => p.Favorites)
            .Where(p => p.HostId == hostId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetByCityAsync(string city)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Reservations)
            .Include(p => p.Reviews)
            .Include(p => p.Photos)
            .Include(p => p.Favorites)
            .Where(p => p.City == city)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetByPropertyTypeAsync(Domain.Enums.PropertyType propertyType)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Reservations)
            .Include(p => p.Reviews)
            .Include(p => p.Photos)
            .Include(p => p.Favorites)
            .Where(p => p.PropertyType == propertyType)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Reservations)
            .Include(p => p.Reviews)
            .Include(p => p.Photos)
            .Include(p => p.Favorites)
            .Where(p => p.PricePerNight >= minPrice && p.PricePerNight <= maxPrice)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetByGuestCountAsync(int guestCount)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Reservations)
            .Include(p => p.Reviews)
            .Include(p => p.Photos)
            .Include(p => p.Favorites)
            .Where(p => p.MaxGuestCount >= guestCount)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetByAmenitiesAsync(bool hasWifi, bool hasAirConditioning, bool hasKitchen, bool hasParking, bool hasPool, bool allowsPets, bool allowsSmoking)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Reservations)
            .Include(p => p.Reviews)
            .Include(p => p.Photos)
            .Include(p => p.Favorites)
            .Where(p => (!hasWifi || p.HasWifi) &&
                       (!hasAirConditioning || p.HasAirConditioning) &&
                       (!hasKitchen || p.HasKitchen) &&
                       (!hasParking || p.HasParking) &&
                       (!hasPool || p.HasPool) &&
                       (!allowsPets || p.AllowsPets) &&
                       (!allowsSmoking || p.AllowsSmoking))
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetByRatingAsync(decimal minRating)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Reservations)
            .Include(p => p.Reviews)
            .Include(p => p.Photos)
            .Include(p => p.Favorites)
            .Where(p => p.Reviews.Any() && p.Reviews.Average(r => (decimal)r.Rating) >= minRating)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetAvailablePropertiesAsync(DateTime checkInDate, DateTime checkOutDate, int guestCount)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Reservations)
            .Include(p => p.Reviews)
            .Include(p => p.Photos)
            .Include(p => p.Favorites)
            .Where(p => p.MaxGuestCount >= guestCount &&
                       !p.Reservations.Any(r => r.Status == Domain.Enums.ReservationStatus.Confirmed &&
                                               r.CheckInDate < checkOutDate &&
                                               r.CheckOutDate > checkInDate))
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetPopularPropertiesAsync(int take = 10)
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Reservations)
            .Include(p => p.Reviews)
            .Include(p => p.Photos)
            .Include(p => p.Favorites)
            .OrderByDescending(p => p.Reservations.Count)
            .ThenByDescending(p => p.Reviews.Average(r => (decimal)r.Rating))
            .Take(take)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> GetPublishedPropertiesAsync()
    {
        return await _context.Properties
            .Include(p => p.Host)
            .Include(p => p.Reservations)
            .Include(p => p.Reviews)
            .Include(p => p.Photos)
            .Include(p => p.Favorites)
            .ToListAsync();
    }

    public async Task<IEnumerable<Property>> SearchPropertiesAsync(string? city = null, Domain.Enums.PropertyType? propertyType = null, 
        decimal? minPrice = null, decimal? maxPrice = null, int? guestCount = null, bool? hasWifi = null, 
        bool? hasAirConditioning = null, bool? hasKitchen = null, bool? hasParking = null, bool? hasPool = null, 
        bool? allowsPets = null, bool? allowsSmoking = null)
    {
        var query = _context.Properties.AsQueryable();

        query = query.Include(p => p.Host)
                     .Include(p => p.Reservations)
                     .Include(p => p.Reviews)
                     .Include(p => p.Photos)
                     .Include(p => p.Favorites);

        if (!string.IsNullOrEmpty(city))
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

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Properties.AnyAsync(p => p.Id == id);
    }
} 