using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.PropertyPhoto.Commands.DeletePropertyPhoto;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.PropertyPhoto.Commands.DeletePropertyPhoto;

/// <summary>
/// PropertyPhoto silme command handler'ı
/// </summary>
public class DeletePropertyPhotoCommandHandler : IRequestHandler<DeletePropertyPhotoCommand, Result<object>>
{
    private readonly IPropertyPhotoRepository _propertyPhotoRepository;

    public DeletePropertyPhotoCommandHandler(IPropertyPhotoRepository propertyPhotoRepository)
    {
        _propertyPhotoRepository = propertyPhotoRepository;
    }

    public async Task<Result<object>> Handle(DeletePropertyPhotoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var propertyPhoto = await _propertyPhotoRepository.GetByIdAsync(request.Id);
            
            if (propertyPhoto == null)
            {
                return Result<object>.Failure(new Error("404", "Belirtilen ID'ye sahip ev fotoğrafı sistemde mevcut değil."));
            }

            await _propertyPhotoRepository.DeleteAsync(request.Id);
            await _propertyPhotoRepository.SaveChangesAsync();

            return Result<object>.Success(true, new SuccessMessage("200", "Ev fotoğrafı başarıyla silindi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Ev fotoğrafı silinirken hata oluştu: {ex.Message}"));
        }
    }
} 