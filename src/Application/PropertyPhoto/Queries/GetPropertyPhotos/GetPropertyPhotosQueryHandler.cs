using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.PropertyPhoto.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Extensions;

namespace MinimalAirbnb.Application.PropertyPhoto.Queries.GetPropertyPhotos;

/// <summary>
/// PropertyPhotos listesi query handler'ı
/// </summary>
public class GetPropertyPhotosQueryHandler : IRequestHandler<GetPropertyPhotosQuery, Result<PagedList<PropertyPhotoDto>>>
{
    private readonly IPropertyPhotoRepository _propertyPhotoRepository;

    public GetPropertyPhotosQueryHandler(IPropertyPhotoRepository propertyPhotoRepository)
    {
        _propertyPhotoRepository = propertyPhotoRepository;
    }

    public async Task<Result<PagedList<PropertyPhotoDto>>> Handle(GetPropertyPhotosQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _propertyPhotoRepository.GetAll();

            // Property ID filtresi
            if (request.PropertyId.HasValue)
            {
                query = query.Where(p => p.PropertyId == request.PropertyId.Value);
            }

            // Sadece ana fotoğraflar filtresi
            if (request.IsMainOnly.HasValue && request.IsMainOnly.Value)
            {
                query = query.Where(p => p.IsMainPhoto);
            }

            // Sıralama
            query = query.OrderBy(p => p.SortOrder);

            // Sayfalama
            var pagedResult = await query.ToPagedListAsync(request.PageNumber - 1, request.PageSize);

            if (!pagedResult.Data.Any())
            {
                var emptyResult = new PagedList<PropertyPhotoDto>(new List<PropertyPhotoDto>(), pagedResult.TotalCount, pagedResult.PageIndex + 1, pagedResult.PageSize);
                return Result<PagedList<PropertyPhotoDto>>.Success(emptyResult, new SuccessMessage("200", "Sistemde ev fotoğrafı bulunamadı."));
            }

            var propertyPhotoDtos = pagedResult.Data.Select(p => new PropertyPhotoDto
            {
                Id = p.Id,
                PropertyId = p.PropertyId,
                PropertyTitle = p.Property?.Title ?? string.Empty,
                PhotoUrl = p.PhotoUrl,
                Title = p.Caption ?? string.Empty,
                Description = p.Caption ?? string.Empty,
                IsMain = p.IsMainPhoto,
                Order = p.SortOrder,
                FileSize = p.FileSize,
                FileType = p.FileType,
                CreatedAt = p.CreatedDate,
                UpdatedAt = p.ModifiedDate
            }).ToList();

            var result = new PagedList<PropertyPhotoDto>(propertyPhotoDtos, pagedResult.TotalCount, pagedResult.PageIndex + 1, pagedResult.PageSize);

            return Result<PagedList<PropertyPhotoDto>>.Success(result, new SuccessMessage("200", "Ev fotoğrafları başarıyla getirildi."));
        }
        catch (Exception ex)
        {
            return Result<PagedList<PropertyPhotoDto>>.Failure(new Error("500", $"Ev fotoğrafları getirilirken hata oluştu: {ex.Message}"));
        }
    }
} 