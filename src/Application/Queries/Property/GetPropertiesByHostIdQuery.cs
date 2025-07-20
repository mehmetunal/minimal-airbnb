using MediatR;
using MinimalAirbnb.Application.DTOs.Property;

namespace MinimalAirbnb.Application.Queries.Property;

/// <summary>
/// Ev Sahibine Göre Evleri Getirme Sorgusu
/// </summary>
public class GetPropertiesByHostIdQuery : IRequest<IEnumerable<PropertyListDto>>
{
    /// <summary>
    /// Ev sahibi ID
    /// </summary>
    public Guid HostId { get; set; }
    
    /// <summary>
    /// Sayfa numarası
    /// </summary>
    public int PageNumber { get; set; } = 1;
    
    /// <summary>
    /// Sayfa boyutu
    /// </summary>
    public int PageSize { get; set; } = 10;
} 