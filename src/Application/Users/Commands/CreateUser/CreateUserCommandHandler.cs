using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;

namespace MinimalAirbnb.Application.Users.Commands.CreateUser;

/// <summary>
/// User oluşturma command handler'ı
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<object>>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<object>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.Email,
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                NormalizedUserName = request.Email.ToUpper(),
                EmailConfirmed = true,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                UserType = Domain.Enums.UserType.Guest,
                IsVerified = false,
                IsActive = true
            };

            var createdUser = await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return Result<object>.Success(createdUser.Id, new SuccessMessage("200", "Kullanıcı sisteme kaydedildi."));
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(new Error("500", $"Kullanıcı oluşturulurken hata oluştu: {ex.Message}"));
        }
    }
} 