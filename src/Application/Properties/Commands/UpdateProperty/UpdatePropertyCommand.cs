using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Properties.Commands.UpdateProperty;

/// <summary>
/// Property güncelleme command'i
/// </summary>
public class UpdatePropertyCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PropertyType { get; set; } = string.Empty;
    public decimal PricePerNight { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int MaxGuests { get; set; }
    public int MinimumStay { get; set; }
    public int MaximumStay { get; set; }
} 