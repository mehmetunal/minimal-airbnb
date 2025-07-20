namespace MinimalAirbnb.Application.Common.Interfaces;

/// <summary>
/// Mevcut kullanıcı servisi interface'i
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Mevcut kullanıcı ID'si
    /// </summary>
    Guid? UserId { get; }

    /// <summary>
    /// Mevcut kullanıcı email'i
    /// </summary>
    string? UserEmail { get; }

    /// <summary>
    /// Kullanıcı giriş yapmış mı?
    /// </summary>
    bool IsAuthenticated { get; }
} 