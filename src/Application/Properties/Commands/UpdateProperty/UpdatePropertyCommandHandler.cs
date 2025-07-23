using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Properties.Commands.UpdateProperty;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Properties.Commands.UpdateProperty;

/// <summary>
/// Property güncelleme command handler'ı
/// </summary>
public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Result<object>>
{
    private readonly IPropertyRepository _propertyRepository;

    public UpdatePropertyCommandHandler(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result<object>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var property = await _propertyRepository.GetByIdAsync(request.Id);
            
            if (property == null)
            {
                return Result<object>.Failure(new Error("404", "Belirtilen ID'ye sahip property sistemde mevcut değil."));
            }

            if (!Enum.TryParse<PropertyType>(request.PropertyType, out var propertyType))
            {
                return Result<object>.Failure(new Error("400", "Belirtilen property tipi sistemde tanımlı değil."));
            }

            property.Title = request.Title;
            property.Description = request.Description;
            property.PropertyType = propertyType;
            property.PricePerNight = request.PricePerNight;
            property.Address = request.Address;
            property.City = request.City;
            property.Country = request.Country;
            property.PostalCode = request.PostalCode;
            property.Latitude = (double)request.Latitude;
            property.Longitude = (double)request.Longitude;
            property.BedroomCount = request.Bedrooms;
            property.BathroomCount = request.Bathrooms;
            property.MaxGuestCount = request.MaxGuests;
            property.MinimumStayDays = request.MinimumStay;
            property.MaximumStayDays = request.MaximumStay;

            await _propertyRepository.UpdateAsync(property);
            await _propertyRepository.SaveChangesAsync();

            return Result<object>.Success(true, new SuccessMessage("200", "Property bilgileri sisteme kaydedildi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Property güncellenirken hata oluştu: {ex.Message}"));
        }
    }
}
