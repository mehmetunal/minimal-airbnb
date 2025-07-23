using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Properties.Commands.CreateProperty;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Properties.Commands.CreateProperty;

/// <summary>
/// Property oluşturma command handler'ı
/// </summary>
public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Result<object>>
{
    private readonly IPropertyRepository _propertyRepository;

    public CreatePropertyCommandHandler(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result<object>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!Enum.TryParse<PropertyType>(request.PropertyType, out var propertyType))
            {
                return Result<object>.Failure(new Error("400", "Belirtilen property tipi sistemde tanımlı değil."));
            }

            var property = new MinimalAirbnb.Domain.Entities.Property
            {
                HostId = Guid.NewGuid(), // Default host ID - gerçek uygulamada current user'dan alınmalı
                Title = request.Title,
                Description = request.Description,
                PropertyType = propertyType,
                PricePerNight = request.PricePerNight,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                PostalCode = request.PostalCode,
                Latitude = (double)request.Latitude,
                Longitude = (double)request.Longitude,
                BedroomCount = request.Bedrooms,
                BathroomCount = request.Bathrooms,
                MaxGuestCount = request.MaxGuests,
                MinimumStayDays = request.MinimumStay,
                MaximumStayDays = request.MaximumStay
            };

            var createdProperty = await _propertyRepository.AddAsync(property);
            await _propertyRepository.SaveChangesAsync();

            return Result<object>.Success(createdProperty.Id, new SuccessMessage("200", "Property sisteme kaydedildi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Property oluşturulurken hata oluştu: {ex.Message}"));
        }
    }
}
