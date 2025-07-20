using MediatR;
using MinimalAirbnb.Application.Common.Models;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Properties.Commands.CreateProperty;

/// <summary>
/// Property oluşturma command'i
/// </summary>
public class CreatePropertyCommand : IRequest<ApiResponse<Guid>>
{
    /// <summary>
    /// Başlık
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Açıklama
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Ev tipi
    /// </summary>
    public PropertyType PropertyType { get; set; }

    /// <summary>
    /// Oda sayısı
    /// </summary>
    public int BedroomCount { get; set; }

    /// <summary>
    /// Yatak sayısı
    /// </summary>
    public int BedCount { get; set; }

    /// <summary>
    /// Banyo sayısı
    /// </summary>
    public int BathroomCount { get; set; }

    /// <summary>
    /// Maksimum misafir sayısı
    /// </summary>
    public int MaxGuestCount { get; set; }

    /// <summary>
    /// Günlük fiyat
    /// </summary>
    public decimal PricePerNight { get; set; }

    /// <summary>
    /// Temizlik ücreti
    /// </summary>
    public decimal CleaningFee { get; set; }

    /// <summary>
    /// Hizmet ücreti
    /// </summary>
    public decimal ServiceFee { get; set; }

    /// <summary>
    /// Adres
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Şehir
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Ülke
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Posta kodu
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// Enlem
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Boylam
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// WiFi var mı?
    /// </summary>
    public bool HasWifi { get; set; }

    /// <summary>
    /// Klima var mı?
    /// </summary>
    public bool HasAirConditioning { get; set; }

    /// <summary>
    /// Mutfak var mı?
    /// </summary>
    public bool HasKitchen { get; set; }

    /// <summary>
    /// Otopark var mı?
    /// </summary>
    public bool HasParking { get; set; }

    /// <summary>
    /// Havuz var mı?
    /// </summary>
    public bool HasPool { get; set; }

    /// <summary>
    /// Evcil hayvan izni var mı?
    /// </summary>
    public bool AllowsPets { get; set; }

    /// <summary>
    /// Sigara izni var mı?
    /// </summary>
    public bool AllowsSmoking { get; set; }

    /// <summary>
    /// Minimum konaklama süresi
    /// </summary>
    public int MinStayDays { get; set; }

    /// <summary>
    /// Maksimum konaklama süresi
    /// </summary>
    public int MaxStayDays { get; set; }

    /// <summary>
    /// İptal süresi (gün)
    /// </summary>
    public int CancellationDays { get; set; }
} 