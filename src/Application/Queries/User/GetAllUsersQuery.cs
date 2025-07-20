using MediatR;
using MinimalAirbnb.Application.DTOs.User;

namespace MinimalAirbnb.Application.Queries.User;

/// <summary>
/// Tüm Kullanıcıları Getirme Sorgusu
/// </summary>
public class GetAllUsersQuery : IRequest<IEnumerable<UserListDto>>
{
    /// <summary>
    /// Sayfa numarası
    /// </summary>
    public int PageNumber { get; set; } = 1;
    
    /// <summary>
    /// Sayfa boyutu
    /// </summary>
    public int PageSize { get; set; } = 10;
    
    /// <summary>
    /// Arama terimi
    /// </summary>
    public string? SearchTerm { get; set; }
    
    /// <summary>
    /// Kullanıcı tipi filtresi
    /// </summary>
    public string? UserType { get; set; }
} 