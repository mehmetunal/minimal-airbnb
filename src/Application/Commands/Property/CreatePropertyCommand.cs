using MediatR;
using MinimalAirbnb.Application.DTOs.Property;
using MinimalAirbnb.Domain.Enums;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Commands.Property;

/// <summary>
/// Ev Oluşturma Komutu
/// </summary>
public class CreatePropertyCommand : IRequest<Result<PropertyResultDto>>
{
    /// <summary>
    /// Ev başlığı
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Ev açıklaması
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Ev sahibi ID'si
    /// </summary>
    public Guid HostId { get; set; }
    
    /// <summary>
    /// Ev tipi
    /// </summary>
    public PropertyType PropertyType { get; set; }
    
    /// <summary>
    /// Oda sayısı
    /// </summary>
    public int BedroomCount { get; set; }
    
    /// <summary>
    /// Yatak odası sayısı (alias)
    /// </summary>
    public int Bedrooms => BedroomCount;
    
    /// <summary>
    /// Yatak sayısı
    /// </summary>
    public int BedCount { get; set; }
    
    /// <summary>
    /// Banyo sayısı
    /// </summary>
    public int BathroomCount { get; set; }
    
    /// <summary>
    /// Banyo sayısı (alias)
    /// </summary>
    public int Bathrooms => BathroomCount;
    
    /// <summary>
    /// Maksimum misafir sayısı
    /// </summary>
    public int MaxGuestCount { get; set; }
    
    /// <summary>
    /// Maksimum misafir sayısı (alias)
    /// </summary>
    public int MaxGuests => MaxGuestCount;
    
    /// <summary>
    /// Günlük fiyat
    /// </summary>
    public decimal PricePerNight { get; set; }

    /// <summary>
    /// Fiyat (alias)
    /// </summary>
    public decimal Price => PricePerNight;
    
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
    public string PostalCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Enlem (Latitude)
    /// </summary>
    public double Latitude { get; set; }
    
    /// <summary>
    /// Boylam (Longitude)
    /// </summary>
    public double Longitude { get; set; }
    
    /// <summary>
    /// Wi-Fi var mı?
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
    /// Çamaşır makinesi var mı?
    /// </summary>
    public bool HasWasher { get; set; }
    
    /// <summary>
    /// Kurutucu var mı?
    /// </summary>
    public bool HasDryer { get; set; }
    
    /// <summary>
    /// Otopark var mı?
    /// </summary>
    public bool HasParking { get; set; }
    
    /// <summary>
    /// Havuz var mı?
    /// </summary>
    public bool HasPool { get; set; }
    
    /// <summary>
    /// Evcil hayvan kabul ediliyor mu?
    /// </summary>
    public bool AllowsPets { get; set; }
    
    /// <summary>
    /// Sigara içilir mi?
    /// </summary>
    public bool AllowsSmoking { get; set; }
    
    /// <summary>
    /// Minimum konaklama süresi (gün)
    /// </summary>
    public int MinimumStay { get; set; } = 1;
    
    /// <summary>
    /// Maksimum konaklama süresi (gün)
    /// </summary>
    public int MaximumStay { get; set; } = 365;
    
    /// <summary>
    /// Check-in saati
    /// </summary>
    public TimeSpan CheckInTime { get; set; } = new TimeSpan(15, 0, 0);
    
    /// <summary>
    /// Check-out saati
    /// </summary>
    public TimeSpan CheckOutTime { get; set; } = new TimeSpan(11, 0, 0);

    /// <summary>
    /// Güvenlik kamerası var mı?
    /// </summary>
    public bool HasSecurityCamera { get; set; }

    /// <summary>
    /// Yangın söndürücü var mı?
    /// </summary>
    public bool HasFireExtinguisher { get; set; }

    /// <summary>
    /// İlk yardım çantası var mı?
    /// </summary>
    public bool HasFirstAidKit { get; set; }
} 