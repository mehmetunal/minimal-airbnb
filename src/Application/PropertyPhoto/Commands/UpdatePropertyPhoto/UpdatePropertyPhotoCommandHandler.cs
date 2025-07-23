using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Commands.PropertyPhoto;
using MinimalAirbnb.Application.DTOs.PropertyPhoto;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.PropertyPhoto.Commands.UpdatePropertyPhoto;

/// <summary>
/// PropertyPhoto güncelleme command handler'ı
/// </summary>
public class UpdatePropertyPhotoCommandHandler : IRequestHandler<UpdatePropertyPhotoCommand, Result<PropertyPhotoResultDto>>
{
    private readonly IPropertyPhotoRepository _propertyPhotoRepository;

    public UpdatePropertyPhotoCommandHandler(IPropertyPhotoRepository propertyPhotoRepository)
    {
        _propertyPhotoRepository = propertyPhotoRepository;
    }

    public async Task<Result<PropertyPhotoResultDto>> Handle(UpdatePropertyPhotoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var propertyPhoto = await _propertyPhotoRepository.GetByIdAsync(request.Id);
            
            if (propertyPhoto == null)
            {
                return Result<PropertyPhotoResultDto>.Failure(new Error("404", "Belirtilen ID'ye sahip ev fotoğrafı sistemde mevcut değil."));
            }

            propertyPhoto.PhotoUrl = request.UpdatePropertyPhotoDto.PhotoUrl;
            propertyPhoto.Caption = request.UpdatePropertyPhotoDto.Title;
            propertyPhoto.IsMainPhoto = request.UpdatePropertyPhotoDto.IsMain;
            propertyPhoto.SortOrder = request.UpdatePropertyPhotoDto.Order;
            propertyPhoto.ModifiedDate = DateTime.UtcNow;

            await _propertyPhotoRepository.UpdateAsync(propertyPhoto);
            await _propertyPhotoRepository.SaveChangesAsync();

            var resultDto = new PropertyPhotoResultDto
            {
                Id = propertyPhoto.Id,
                PropertyId = propertyPhoto.PropertyId,
                PhotoUrl = propertyPhoto.PhotoUrl,
                Title = propertyPhoto.Caption ?? string.Empty,
                IsMain = propertyPhoto.IsMainPhoto,
                Order = propertyPhoto.SortOrder,
                CreatedAt = propertyPhoto.CreatedDate,
                UpdatedAt = propertyPhoto.ModifiedDate
            };

            return Result<PropertyPhotoResultDto>.Success(resultDto, new SuccessMessage("200", "Ev fotoğrafı başarıyla güncellendi."));
        }
        catch (Exception ex)
        {
            return Result<PropertyPhotoResultDto>.Failure(new Error("500", $"Ev fotoğrafı güncellenirken hata oluştu: {ex.Message}"));
        }
    }
} 