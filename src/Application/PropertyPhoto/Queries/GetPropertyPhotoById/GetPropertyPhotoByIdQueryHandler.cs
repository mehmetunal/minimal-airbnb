using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.PropertyPhoto.Queries.GetPropertyPhotoById;
using MinimalAirbnb.Application.PropertyPhoto.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.PropertyPhoto.Queries.GetPropertyPhotoById;

/// <summary>
/// PropertyPhoto by ID query handler'ı
/// </summary>
public class GetPropertyPhotoByIdQueryHandler : IRequestHandler<GetPropertyPhotoByIdQuery, Result<PropertyPhotoDto>>
{
    private readonly IPropertyPhotoRepository _propertyPhotoRepository;

    public GetPropertyPhotoByIdQueryHandler(IPropertyPhotoRepository propertyPhotoRepository)
    {
        _propertyPhotoRepository = propertyPhotoRepository;
    }

    public async Task<Result<PropertyPhotoDto>> Handle(GetPropertyPhotoByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var propertyPhoto = await _propertyPhotoRepository.GetByIdAsync(request.Id);
            
            if (propertyPhoto == null)
            {
                return Result<PropertyPhotoDto>.Failure(new Error("404", "Belirtilen ID'ye sahip ev fotoğrafı sistemde mevcut değil."));
            }

            var propertyPhotoDto = new PropertyPhotoDto
            {
                Id = propertyPhoto.Id,
                PropertyId = propertyPhoto.PropertyId,
                PropertyTitle = propertyPhoto.Property?.Title ?? string.Empty,
                PhotoUrl = propertyPhoto.PhotoUrl,
                Title = propertyPhoto.Caption ?? string.Empty,
                Description = propertyPhoto.Caption ?? string.Empty,
                IsMain = propertyPhoto.IsMainPhoto,
                Order = propertyPhoto.SortOrder,
                FileSize = propertyPhoto.FileSize,
                FileType = propertyPhoto.FileType,
                CreatedAt = propertyPhoto.CreatedDate,
                UpdatedAt = propertyPhoto.ModifiedDate
            };

            return Result<PropertyPhotoDto>.Success(propertyPhotoDto, new SuccessMessage("200", "Ev fotoğrafı bilgileri başarıyla getirildi."));
        }
        catch (Exception ex)
        {
            return Result<PropertyPhotoDto>.Failure(new Error("500", $"Ev fotoğrafı getirilirken hata oluştu: {ex.Message}"));
        }
    }
} 