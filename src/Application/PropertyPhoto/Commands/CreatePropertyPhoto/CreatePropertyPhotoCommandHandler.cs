using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Commands.PropertyPhoto;
using MinimalAirbnb.Application.DTOs.PropertyPhoto;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.PropertyPhoto.Commands.CreatePropertyPhoto;

/// <summary>
/// PropertyPhoto oluşturma command handler'ı
/// </summary>
public class CreatePropertyPhotoCommandHandler : IRequestHandler<CreatePropertyPhotoCommand, Result<PropertyPhotoResultDto>>
{
    private readonly IPropertyPhotoRepository _propertyPhotoRepository;

    public CreatePropertyPhotoCommandHandler(IPropertyPhotoRepository propertyPhotoRepository)
    {
        _propertyPhotoRepository = propertyPhotoRepository;
    }

    public async Task<Result<PropertyPhotoResultDto>> Handle(CreatePropertyPhotoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var propertyPhoto = new MinimalAirbnb.Domain.Entities.PropertyPhoto
            {
                PropertyId = request.AddPropertyPhotoDto.PropertyId,
                PhotoUrl = request.AddPropertyPhotoDto.PhotoUrl,
                Caption = request.AddPropertyPhotoDto.Title,
                IsMainPhoto = request.AddPropertyPhotoDto.IsMain,
                SortOrder = request.AddPropertyPhotoDto.Order,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };

            var createdPropertyPhoto = await _propertyPhotoRepository.AddAsync(propertyPhoto);
            await _propertyPhotoRepository.SaveChangesAsync();

            var resultDto = new PropertyPhotoResultDto
            {
                Id = createdPropertyPhoto.Id,
                PropertyId = createdPropertyPhoto.PropertyId,
                PhotoUrl = createdPropertyPhoto.PhotoUrl,
                Title = createdPropertyPhoto.Caption ?? string.Empty,
                IsMain = createdPropertyPhoto.IsMainPhoto,
                Order = createdPropertyPhoto.SortOrder,
                CreatedAt = createdPropertyPhoto.CreatedDate,
                UpdatedAt = createdPropertyPhoto.ModifiedDate
            };

            return Result<PropertyPhotoResultDto>.Success(resultDto, new SuccessMessage("200", "Ev fotoğrafı sisteme kaydedildi."));
        }
        catch (Exception ex)
        {
            return Result<PropertyPhotoResultDto>.Failure(new Error("500", $"Ev fotoğrafı oluşturulurken hata oluştu: {ex.Message}"));
        }
    }
} 