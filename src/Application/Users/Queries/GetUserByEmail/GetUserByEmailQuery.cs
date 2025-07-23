using MediatR;
using MinimalAirbnb.Application.Users.DTOs;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Users.Queries.GetUserByEmail;

/// <summary>
/// User by Email query
/// </summary>
public class GetUserByEmailQuery : IRequest<Result<UserDto>>
{
    /// <summary>
    /// Email adresi
    /// </summary>
    public string Email { get; set; } = string.Empty;
} 