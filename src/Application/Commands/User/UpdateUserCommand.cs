using MediatR;
using MinimalAirbnb.Application.DTOs.User;

namespace MinimalAirbnb.Application.Commands.User;

/// <summary>
/// Kullanıcı Güncelleme Komutu
/// </summary>
public class UpdateUserCommand : IRequest<UserResultDto>
{
    /// <summary>
    /// Kullanıcı güncelleme DTO
    /// </summary>
    public UpdateUserDto UpdateUserDto { get; set; } = new();
} 