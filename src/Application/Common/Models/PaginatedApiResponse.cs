namespace MinimalAirbnb.Application.Common.Models;

/// <summary>
/// Pagination için response modeli
/// </summary>
public class PaginatedApiResponse<T> : ApiResponse<List<T>>
{
    /// <summary>
    /// Toplam kayıt sayısı
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Sayfa numarası
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Sayfa boyutu
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Toplam sayfa sayısı
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Önceki sayfa var mı?
    /// </summary>
    public bool HasPreviousPage { get; set; }

    /// <summary>
    /// Sonraki sayfa var mı?
    /// </summary>
    public bool HasNextPage { get; set; }
} 
