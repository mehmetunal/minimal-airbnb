using MediatR;
using Maggsoft.Core.Base;
using MinimalAirbnb.Application.Users.DTOs;

namespace MinimalAirbnb.Application.Users.Queries.GetUserById;

/// <summary>
/// User detayı query'si
/// </summary>
public class GetUserByIdQuery : IRequest<Result<UserDto>>
{
    public Guid Id { get; set; }
} 