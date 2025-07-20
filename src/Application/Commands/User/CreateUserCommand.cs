using MediatR;
using MinimalAirbnb.Application.DTOs.User;

namespace MinimalAirbnb.Application.Commands.User;

/// <summary>
/// Kullanıcı Oluşturma Komutu
/// </summary>
public class CreateUserCommand : IRequest<UserResultDto>
{
    /// <summary>
    /// Kullanıcı ekleme DTO
    /// </summary>
    public AddUserDto AddUserDto { get; set; } = new();
} 