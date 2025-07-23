using MediatR;
using MinimalAirbnb.Application.Interfaces;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Users.Commands.UpdateUser;

/// <summary>
/// User güncelleme command handler'ı
/// </summary>
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<object>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<object>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                return Result<object>.Failure(new Error("404", "Kullanıcı bulunamadı."));
            }

            // Email kontrolü (kendi email'i hariç)
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null && existingUser.Id != request.Id)
            {
                return Result<object>.Failure(new Error("400", "Bu email adresi başka bir kullanıcı tarafından kullanılıyor."));
            }

            // User bilgilerini güncelle
            existingUser.FirstName = request.FirstName;
            existingUser.LastName = request.LastName;
            existingUser.PhoneNumber = request.PhoneNumber;
            existingUser.DateOfBirth = request.DateOfBirth;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return Result<object>.Success(true, new SuccessMessage("200", "Kullanıcı başarıyla güncellendi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Kullanıcı güncellenirken hata oluştu: {ex.Message}"));
        }
    }
} 