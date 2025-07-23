using Microsoft.EntityFrameworkCore;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Infrastructure.Data;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// PropertyPhoto Repository Implementation
/// </summary>
public class PropertyPhotoRepository : IPropertyPhotoRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public PropertyPhotoRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    public IQueryable<Domain.Entities.PropertyPhoto> GetAll()
    {
        return _context.PropertyPhotos.Include(p => p.Property);
    }

    public async Task<Domain.Entities.PropertyPhoto?> GetByIdAsync(Guid id)
    {
        return await _context.PropertyPhotos
            .Include(p => p.Property)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Domain.Entities.PropertyPhoto> AddAsync(Domain.Entities.PropertyPhoto photo)
    {
        await _context.PropertyPhotos.AddAsync(photo);
        return photo;
    }

    public async Task<Domain.Entities.PropertyPhoto> UpdateAsync(Domain.Entities.PropertyPhoto photo)
    {
        _context.PropertyPhotos.Update(photo);
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

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Entities.PropertyPhoto>> GetByPropertyIdAsync(Guid propertyId)
    {
        return await _context.PropertyPhotos
            .Include(p => p.Property)
            .Where(p => p.PropertyId == propertyId)
            .OrderBy(p => p.SortOrder)
            .ToListAsync();
    }

    public async Task<Domain.Entities.PropertyPhoto?> GetMainPhotoAsync(Guid propertyId)
    {
        return await _context.PropertyPhotos
            .Include(p => p.Property)
            .Where(p => p.PropertyId == propertyId && p.IsMainPhoto)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Domain.Entities.PropertyPhoto>> GetMainPhotosAsync()
    {
        return await _context.PropertyPhotos
            .Include(p => p.Property)
            .Where(p => p.IsMainPhoto)
            .ToListAsync();
    }

    public async Task<int> GetPhotoCountByPropertyAsync(Guid propertyId)
    {
        return await _context.PropertyPhotos
            .CountAsync(p => p.PropertyId == propertyId);
    }

    public async Task UpdateOrderAsync(Guid photoId, int newOrder)
    {
        var photo = await _context.PropertyPhotos.FindAsync(photoId);
        if (photo != null)
        {
            photo.SortOrder = newOrder;
            _context.PropertyPhotos.Update(photo);
        }
    }

    public async Task SetMainPhotoAsync(Guid photoId)
    {
        var photo = await _context.PropertyPhotos.FindAsync(photoId);
        if (photo != null)
        {
            // Önce aynı property'deki diğer fotoğrafları main olmaktan çıkar
            var otherPhotos = await _context.PropertyPhotos
                .Where(p => p.PropertyId == photo.PropertyId && p.IsMainPhoto)
                .ToListAsync();

            foreach (var otherPhoto in otherPhotos)
            {
                otherPhoto.IsMainPhoto = false;
                _context.PropertyPhotos.Update(otherPhoto);
            }

            // Seçilen fotoğrafı main yap
            photo.IsMainPhoto = true;
            _context.PropertyPhotos.Update(photo);
        }
    }
} 