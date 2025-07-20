using MediatR;
using MinimalAirbnb.Application.DTOs.Property;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Queries.Property;

/// <summary>
/// Tüm Evleri Getirme Sorgusu
/// </summary>
public class GetAllPropertiesQuery : IRequest<IEnumerable<PropertyListDto>>
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
    /// Şehir filtresi
    /// </summary>
    public string? City { get; set; }
    
    /// <summary>
    /// Ev tipi filtresi
    /// </summary>
    public PropertyType? PropertyType { get; set; }
    
    /// <summary>
    /// Minimum fiyat
    /// </summary>
    public decimal? MinPrice { get; set; }
    
    /// <summary>
    /// Maksimum fiyat
    /// </summary>
    public decimal? MaxPrice { get; set; }
    
    /// <summary>
    /// Misafir sayısı
    /// </summary>
    public int? GuestCount { get; set; }
    
    /// <summary>
    /// WiFi var mı?
    /// </summary>
    public bool? HasWifi { get; set; }
    
    /// <summary>
    /// Klima var mı?
    /// </summary>
    public bool? HasAirConditioning { get; set; }
    
    /// <summary>
    /// Mutfak var mı?
    /// </summary>
    public bool? HasKitchen { get; set; }
    
    /// <summary>
    /// Otopark var mı?
    /// </summary>
    public bool? HasParking { get; set; }
    
    /// <summary>
    /// Havuz var mı?
    /// </summary>
    public bool? HasPool { get; set; }
    
    /// <summary>
    /// Evcil hayvan izni var mı?
    /// </summary>
    public bool? AllowsPets { get; set; }
    
    /// <summary>
    /// Sigara izni var mı?
    /// </summary>
    public bool? AllowsSmoking { get; set; }
    
    /// <summary>
    /// Sadece yayınlanmış evler
    /// </summary>
    public bool OnlyPublished { get; set; } = true;
} 