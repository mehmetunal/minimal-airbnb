using MediatR;
using MinimalAirbnb.Application.Common.Interfaces;
using MinimalAirbnb.Application.Common.Models;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Properties.Commands.UpdateProperty;

/// <summary>
/// UpdatePropertyCommand için handler
/// </summary>
public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, ApiResponse<bool>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdatePropertyCommandHandler(
        IPropertyRepository propertyRepository,
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<bool>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Mevcut kullanıcıyı al
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Kullanıcı girişi yapılmamış"
                };
            }

            // Property'yi getir
            var properties = await _propertyRepository.GetPublishedPropertiesAsync();
            var property = properties.FirstOrDefault(p => p.Id == request.Id);

            if (property == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Ev bulunamadı"
                };
            }

            // Kullanıcının yetkisini kontrol et
            var user = await _userRepository.GetByIdAsync(currentUserId.Value);
            if (user == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Kullanıcı bulunamadı"
                };
            }

            // Sadece ev sahibi veya admin güncelleyebilir
            if (property.HostId != currentUserId.Value && user.UserType != UserType.Admin)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Bu evi güncelleme yetkiniz yok"
                };
            }

            // Property'yi güncelle
            property.Title = request.Title;
            property.Description = request.Description;
            property.PropertyType = request.PropertyType;
            property.BedroomCount = request.BedroomCount;
            property.BedCount = request.BedCount;
            property.BathroomCount = request.BathroomCount;
            property.MaxGuestCount = request.MaxGuestCount;
            property.PricePerNight = request.PricePerNight;
            property.CleaningFee = request.CleaningFee;
            property.ServiceFee = request.ServiceFee;
            property.Address = request.Address;
            property.City = request.City;
            property.Country = request.Country;
            property.PostalCode = request.PostalCode;
            property.Latitude = request.Latitude;
            property.Longitude = request.Longitude;
            property.HasWifi = request.HasWifi;
            property.HasAirConditioning = request.HasAirConditioning;
            property.HasKitchen = request.HasKitchen;
            property.HasParking = request.HasParking;
            property.HasPool = request.HasPool;
            property.AllowsPets = request.AllowsPets;
            property.AllowsSmoking = request.AllowsSmoking;
            property.MinimumStayDays = request.MinStayDays;
            property.MaximumStayDays = request.MaxStayDays;
            property.CancellationPolicyDays = request.CancellationDays;

            // Değişiklikleri kaydet
            await _propertyRepository.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Ev başarıyla güncellendi",
                Data = true
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                Message = $"Ev güncellenirken hata oluştu: {ex.Message}"
            };
        }
    }
} 