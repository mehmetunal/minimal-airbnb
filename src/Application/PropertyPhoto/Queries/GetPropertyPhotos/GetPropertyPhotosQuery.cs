using MediatR;
using MinimalAirbnb.Application.PropertyPhoto.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;

namespace MinimalAirbnb.Application.PropertyPhoto.Queries.GetPropertyPhotos;

/// <summary>
/// PropertyPhotos listesi query
/// </summary>
public class GetPropertyPhotosQuery : IRequest<Result<PagedList<PropertyPhotoDto>>>
{
    /// <summary>
    /// Ev ID filtresi
    /// </summary>
    public Guid? PropertyId { get; set; }
    
    /// <summary>
    /// Sadece ana fotoğraflar mı?
    /// </summary>
    public bool? IsMainOnly { get; set; }
    
    /// <summary>
    /// Sayfa numarası
    /// </summary>
    public int PageNumber { get; set; } = 1;
    
    /// <summary>
    /// Sayfa boyutu
    /// </summary>
    public int PageSize { get; set; } = 10;
} 