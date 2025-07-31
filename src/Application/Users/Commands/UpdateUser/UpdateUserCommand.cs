using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Domain.Enums;

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
    public UserType UserType { get; set; }
    public Gender Gender { get; set; }
    public bool EmailConfirmed { get; set; }
} 