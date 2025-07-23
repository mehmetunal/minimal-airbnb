using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Properties.Commands.DeleteProperty;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Properties.Commands.DeleteProperty;

/// <summary>
/// Property silme command handler
/// </summary>
public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, Result<object>>
{
    private readonly IPropertyRepository _propertyRepository;

    public DeletePropertyCommandHandler(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result<object>> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Property'yi bul
            var property = await _propertyRepository.GetByIdAsync(request.Id);
            if (property == null)
                return Result<object>.Failure(new Error("404", "Mülk bulunamadı."));

            // Repository'den sil
            await _propertyRepository.DeleteAsync(request.Id);

            return Result<object>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Mülk silinirken hata oluştu: {ex.Message}"));
        }
    }
}
