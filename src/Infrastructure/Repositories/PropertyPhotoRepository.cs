using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Ev Fotoğrafı Repository Implementasyonu
/// </summary>
public class PropertyPhotoRepository : IPropertyPhotoRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public PropertyPhotoRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<PropertyPhoto> AddAsync(PropertyPhoto photo)
    {
        await _context.PropertyPhotos.AddAsync(photo);
        return photo;
    }

    public async Task DeleteAsync(Guid id)
    {
        var photo = await _context.PropertyPhotos.FindAsync(id);
        if (photo != null)
        {
            _context.PropertyPhotos.Remove(photo);
        }
    }

    public async Task<IEnumerable<PropertyPhoto>> GetByPropertyIdAsync(Guid propertyId)
    {
        return await _context.PropertyPhotos
            .Include(p => p.Property)
            .Include(p => p.Property.Host)
            .Where(p => p.PropertyId == propertyId)
            .OrderBy(p => p.SortOrder)
            .ToListAsync();
    }

    public async Task<PropertyPhoto?> GetMainPhotoAsync(Guid propertyId)
    {
        return await _context.PropertyPhotos
            .Include(p => p.Property)
            .Include(p => p.Property.Host)
            .FirstOrDefaultAsync(p => p.PropertyId == propertyId && p.IsMainPhoto);
    }

    public async Task SetMainPhotoAsync(Guid propertyId, Guid photoId)
    {
        // Önce tüm fotoğrafları ana fotoğraf olmaktan çıkar
        var photos = await _context.PropertyPhotos
            .Where(p => p.PropertyId == propertyId)
            .ToListAsync();

        foreach (var photo in photos)
        {
            photo.IsMainPhoto = false;
        }

        // Seçilen fotoğrafı ana fotoğraf yap
        var mainPhoto = await _context.PropertyPhotos
            .FirstOrDefaultAsync(p => p.Id == photoId && p.PropertyId == propertyId);

        if (mainPhoto != null)
        {
            mainPhoto.IsMainPhoto = true;
        }
    }

    public async Task UpdatePhotoOrderAsync(Guid propertyId, Dictionary<Guid, int> photoOrders)
    {
        var photos = await _context.PropertyPhotos
            .Where(p => p.PropertyId == propertyId)
            .ToListAsync();

        foreach (var photo in photos)
        {
            if (photoOrders.ContainsKey(photo.Id))
            {
                photo.SortOrder = photoOrders[photo.Id];
            }
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
} 