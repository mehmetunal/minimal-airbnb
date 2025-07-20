namespace MinimalAirbnb.Domain.Enums;

/// <summary>
/// Kullanıcı tipleri
/// </summary>
public enum UserType
{
    /// <summary>
    /// Misafir - Ev kiralayan kullanıcı
    /// </summary>
    Guest = 1,

    /// <summary>
    /// Ev Sahibi - Ev kiralayan kullanıcı
    /// </summary>
    Host = 2,

    /// <summary>
    /// Yönetici - Sistem yöneticisi
    /// </summary>
    Admin = 3
} 