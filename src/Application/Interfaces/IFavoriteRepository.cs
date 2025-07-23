using MinimalAirbnb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// Favorite Repository Interface
/// </summary>
public interface IFavoriteRepository
{
    /// <summary>
    /// Tüm favorite'ları getir (IQueryable)
    /// </summary>
    IQueryable<Favorite> GetAll();

    /// <summary>
    /// Tüm favorite'ları getir
    /// </summary>
    Task<IEnumerable<Favorite>> GetAllAsync();

    /// <summary>
    /// ID'ye göre favorite getir
    /// </summary>
    Task<Favorite?> GetByIdAsync(Guid id);

    /// <summary>
    /// Kullanıcıya göre favorite'ları getir
    /// </summary>
    Task<IEnumerable<Favorite>> GetByUserAsync(Guid userId);

    /// <summary>
    /// Property'ye göre favorite'ları getir
    /// </summary>
    Task<IEnumerable<Favorite>> GetByPropertyAsync(Guid propertyId);

    /// <summary>
    /// Kullanıcı ve property'ye göre favorite getir
    /// </summary>
    Task<Favorite?> GetByUserAndPropertyAsync(Guid userId, Guid propertyId);

    /// <summary>
    /// Favorite ekle
    /// </summary>
    Task<Favorite> AddAsync(Favorite favorite);

    /// <summary>
    /// Favorite güncelle
    /// </summary>
    Task<Favorite> UpdateAsync(Favorite favorite);

    /// <summary>
    /// Favorite sil
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Değişiklikleri kaydet
    /// </summary>
    Task<int> SaveChangesAsync();
} 