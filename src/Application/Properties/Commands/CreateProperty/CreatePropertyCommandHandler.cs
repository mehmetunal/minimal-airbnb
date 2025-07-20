using MediatR;
using Microsoft.AspNetCore.Identity;
using MinimalAirbnb.Application.Common.Models;
using MinimalAirbnb.Application.Common.Interfaces;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Properties.Commands.CreateProperty;

/// <summary>
/// CreatePropertyCommand için handler
/// </summary>
public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, ApiResponse<Guid>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreatePropertyCommandHandler(
        IPropertyRepository propertyRepository,
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<Guid>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Mevcut kullanıcıyı al
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == null)
            {
                return new ApiResponse<Guid>
                {
                    Success = false,
                    Message = "Kullanıcı girişi yapılmamış"
                };
            }

            // Kullanıcının host olup olmadığını kontrol et
            var user = await _userRepository.GetByIdAsync(currentUserId.Value);
            if (user == null)
            {
                return new ApiResponse<Guid>
                {
                    Success = false,
                    Message = "Kullanıcı bulunamadı"
                };
            }

            if (user.UserType != UserType.Host && user.UserType != UserType.Admin)
            {
                return new ApiResponse<Guid>
                {
                    Success = false,
                    Message = "Sadece host kullanıcılar ev oluşturabilir"
                };
            }

            // Property oluştur
            var property = new Property
            {
                Id = Guid.NewGuid(),
                HostId = currentUserId.Value,
                Title = request.Title,
                Description = request.Description,
                PropertyType = request.PropertyType,
                BedroomCount = request.BedroomCount,
                BedCount = request.BedCount,
                BathroomCount = request.BathroomCount,
                MaxGuestCount = request.MaxGuestCount,
                PricePerNight = request.PricePerNight,
                CleaningFee = request.CleaningFee,
                ServiceFee = request.ServiceFee,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                PostalCode = request.PostalCode,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                HasWifi = request.HasWifi,
                HasAirConditioning = request.HasAirConditioning,
                HasKitchen = request.HasKitchen,
                HasParking = request.HasParking,
                HasPool = request.HasPool,
                AllowsPets = request.AllowsPets,
                AllowsSmoking = request.AllowsSmoking,
                MinimumStayDays = request.MinStayDays,
                MaximumStayDays = request.MaxStayDays,
                CancellationPolicyDays = request.CancellationDays,
                IsPublish = true,
                IsDeleted = false
            };

            // Property'yi repository üzerinden ekle
            await _propertyRepository.AddAsync(property);
            await _propertyRepository.SaveChangesAsync();

            return new ApiResponse<Guid>
            {
                Success = true,
                Message = "Ev başarıyla oluşturuldu",
                Data = property.Id
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<Guid>
            {
                Success = false,
                Message = $"Ev oluşturulurken hata oluştu: {ex.Message}"
            };
        }
    }
} 