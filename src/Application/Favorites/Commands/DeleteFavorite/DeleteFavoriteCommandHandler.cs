using MediatR;
using MinimalAirbnb.Application.Interfaces;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Favorites.Commands.DeleteFavorite;

/// <summary>
/// Favorite silme command handler'ı
/// </summary>
public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, Result<object>>
{
    private readonly IFavoriteRepository _favoriteRepository;

    public DeleteFavoriteCommandHandler(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    public async Task<Result<object>> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var favorite = await _favoriteRepository.GetByIdAsync(request.Id);
            if (favorite == null)
            {
                return Result<object>.Failure(new Error("404", "Favorite bulunamadı."));
            }

            await _favoriteRepository.DeleteAsync(request.Id);
            await _favoriteRepository.SaveChangesAsync();

            return Result<object>.Success(true, new SuccessMessage("200", "Favorite başarıyla silindi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Favorite silinirken hata oluştu: {ex.Message}"));
        }
    }
} 