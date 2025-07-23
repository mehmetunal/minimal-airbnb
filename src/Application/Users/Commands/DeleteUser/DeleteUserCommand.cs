using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Users.Commands.DeleteUser;

/// <summary>
/// User silme command'i
/// </summary>
public class DeleteUserCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
} 