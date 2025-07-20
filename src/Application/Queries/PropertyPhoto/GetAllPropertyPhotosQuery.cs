using MediatR;
using MinimalAirbnb.Application.DTOs.PropertyPhoto;

namespace MinimalAirbnb.Application.Queries.PropertyPhoto;

/// <summary>
/// Tüm Ev Fotoğraflarını Getirme Sorgusu
/// </summary>
public class GetAllPropertyPhotosQuery : IRequest<IEnumerable<PropertyPhotoListDto>>
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
    /// Ev ID filtresi
    /// </summary>
    public Guid? PropertyId { get; set; }
    
    /// <summary>
    /// Ana fotoğraf filtresi
    /// </summary>
    public bool? IsMain { get; set; }
} 