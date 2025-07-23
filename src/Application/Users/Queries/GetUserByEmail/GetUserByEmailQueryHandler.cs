using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Users.Queries.GetUserByEmail;
using MinimalAirbnb.Application.Users.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Users.Queries.GetUserByEmail;

/// <summary>
/// User by Email query handler'ı
/// </summary>
public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            
            if (user == null)
            {
                return Result<UserDto>.Failure(new Error("404", "Belirtilen email adresine sahip kullanıcı sistemde mevcut değil."));
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth
            };

            return Result<UserDto>.Success(userDto, new SuccessMessage("200", "Kullanıcı bilgileri başarıyla getirildi."));
        }
        catch (Exception ex)
        {
            return Result<UserDto>.Failure(new Error("500", $"Kullanıcı getirilirken hata oluştu: {ex.Message}"));
        }
    }
} 