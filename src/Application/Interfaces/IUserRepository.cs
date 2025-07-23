using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Interfaces;

/// <summary>
/// User Repository Interface
/// </summary>
public interface IUserRepository
{
    #region READ
    
    /// <summary>
    /// Tüm kullanıcıları getir (IQueryable)
    /// </summary>
    IQueryable<User> GetAll();
    
    /// <summary>
    /// Tüm kullanıcıları getir
    /// </summary>
    Task<IEnumerable<User>> GetAllAsync();
    
    /// <summary>
    /// ID'ye göre kullanıcı getir
    /// </summary>
    Task<User?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Email ile kullanıcı bul
    /// </summary>
    Task<User?> GetByEmailAsync(string email);
    
    /// <summary>
    /// Kullanıcı adı ile kullanıcı bul
    /// </summary>
    Task<User?> GetByUserNameAsync(string userName);
    
    /// <summary>
    /// Telefon numarası ile kullanıcı bul
    /// </summary>
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    
    /// <summary>
    /// Kullanıcı tipine göre kullanıcıları getir
    /// </summary>
    Task<IEnumerable<User>> GetByUserTypeAsync(UserType userType);
    
    /// <summary>
    /// Aktif kullanıcıları getir
    /// </summary>
    Task<IEnumerable<User>> GetActiveUsersAsync();
    
    /// <summary>
    /// Email onaylanmış kullanıcıları getir
    /// </summary>
    Task<IEnumerable<User>> GetEmailConfirmedUsersAsync();
    
    /// <summary>
    /// Telefon onaylanmış kullanıcıları getir
    /// </summary>
    Task<IEnumerable<User>> GetPhoneConfirmedUsersAsync();
    
    /// <summary>
    /// İki faktörlü doğrulama aktif kullanıcıları getir
    /// </summary>
    Task<IEnumerable<User>> GetTwoFactorEnabledUsersAsync();
    
    /// <summary>
    /// Kilitli kullanıcıları getir
    /// </summary>
    Task<IEnumerable<User>> GetLockedOutUsersAsync();
    
    /// <summary>
    /// Şehre göre kullanıcıları getir
    /// </summary>
    Task<IEnumerable<User>> GetByCityAsync(string city);
    
    /// <summary>
    /// Ev sahiplerini getir
    /// </summary>
    Task<IEnumerable<User>> GetHostsAsync();
    
    /// <summary>
    /// Misafirleri getir
    /// </summary>
    Task<IEnumerable<User>> GetGuestsAsync();
    
    #endregion
    
    #region WRITE
    
    /// <summary>
    /// Kullanıcı ekle
    /// </summary>
    Task<User> AddAsync(User user);
    
    /// <summary>
    /// Kullanıcı güncelle
    /// </summary>
    Task<User> UpdateAsync(User user);
    
    /// <summary>
    /// Kullanıcı sil
    /// </summary>
    Task DeleteAsync(Guid id);
    
    /// <summary>
    /// Değişiklikleri kaydet
    /// </summary>
    Task<int> SaveChangesAsync();
    
    #endregion
} 