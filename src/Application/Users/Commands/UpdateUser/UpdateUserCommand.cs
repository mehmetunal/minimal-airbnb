using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Users.Commands.UpdateUser;

/// <summary>
/// User güncelleme command'i
/// </summary>
public class UpdateUserCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
} 