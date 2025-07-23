using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Favorites.Commands.CreateFavorite;

/// <summary>
/// Favorite oluşturma command handler'ı
/// </summary>
public class CreateFavoriteCommandHandler : IRequestHandler<CreateFavoriteCommand, Result<object>>
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;

    public CreateFavoriteCommandHandler(
        IFavoriteRepository favoriteRepository,
        IPropertyRepository propertyRepository,
        IUserRepository userRepository)
    {
        _favoriteRepository = favoriteRepository;
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<object>> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Property kontrolü
            var property = await _propertyRepository.GetByIdAsync(request.PropertyId);
            if (property == null)
            {
                return Result<object>.Failure(new Error("404", "Property bulunamadı."));
            }

            // User kontrolü
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return Result<object>.Failure(new Error("404", "Kullanıcı bulunamadı."));
            }

            // Daha önce favorite eklenmiş mi kontrolü
            var existingFavorite = await _favoriteRepository.GetByUserAndPropertyAsync(request.UserId, request.PropertyId);
            if (existingFavorite != null)
            {
                return Result<object>.Failure(new Error("400", "Bu property zaten favorilerinizde."));
            }

            // Yeni favorite oluştur
            var favorite = new Favorite
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                PropertyId = request.PropertyId,
                CreatedDate = DateTime.UtcNow
            };

            await _favoriteRepository.AddAsync(favorite);
            await _favoriteRepository.SaveChangesAsync();

            return Result<object>.Success(favorite.Id, new SuccessMessage("200", "Property favorilere eklendi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Favorite oluşturulurken hata oluştu: {ex.Message}"));
        }
    }
} 