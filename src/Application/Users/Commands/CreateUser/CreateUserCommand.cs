using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Users.Commands.CreateUser;

/// <summary>
/// User oluşturma command'i
/// </summary>
public class CreateUserCommand : IRequest<Result<object>>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Password { get; set; } = string.Empty;
} 