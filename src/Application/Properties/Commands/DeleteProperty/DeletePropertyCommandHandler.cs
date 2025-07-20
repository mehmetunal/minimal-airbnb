using MediatR;
using MinimalAirbnb.Application.Common.Interfaces;
using MinimalAirbnb.Application.Common.Models;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Application.Properties.Commands.DeleteProperty;

/// <summary>
/// DeletePropertyCommand için handler
/// </summary>
public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, ApiResponse<bool>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public DeletePropertyCommandHandler(
        IPropertyRepository propertyRepository,
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<bool>> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
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

            // Sadece ev sahibi veya admin silebilir
            if (property.HostId != currentUserId.Value && user.UserType != UserType.Admin)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Bu evi silme yetkiniz yok"
                };
            }

            // Property'yi soft delete yap (IsDeleted = true)
            property.IsDeleted = true;
            property.IsPublish = false;

            // Değişiklikleri kaydet
            await _propertyRepository.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Ev başarıyla silindi",
                Data = true
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                Message = $"Ev silinirken hata oluştu: {ex.Message}"
            };
        }
    }
} 