using MediatR;
using MinimalAirbnb.Application.Interfaces;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Users.Commands.DeleteUser;

/// <summary>
/// User silme command handler'ı
/// </summary>
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<object>>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                return Result<object>.Failure(new Error("404", "Kullanıcı bulunamadı."));
            }

            await _userRepository.DeleteAsync(request.Id);
            await _userRepository.SaveChangesAsync();

            return Result<object>.Success(true, new SuccessMessage("200", "Kullanıcı başarıyla silindi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Kullanıcı silinirken hata oluştu: {ex.Message}"));
        }
    }
} 