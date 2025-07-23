using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Users.Queries.GetUserById;
using MinimalAirbnb.Application.Users.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Users.Queries.GetUserById;

/// <summary>
/// User by ID query handler'ı
/// </summary>
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            
            if (user == null)
            {
                return Result<UserDto>.Failure(new Error("404", "Belirtilen ID'ye sahip kullanıcı sistemde mevcut değil."));
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