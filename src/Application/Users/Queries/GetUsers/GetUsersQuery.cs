using MediatR;
using Maggsoft.Core.Model.Pagination;
using MinimalAirbnb.Application.Users.DTOs;

namespace MinimalAirbnb.Application.Users.Queries.GetUsers;

/// <summary>
/// Users listesi query'si
/// </summary>
public class GetUsersQuery : IRequest<PagedList<UserDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public string? SortBy { get; set; }
    public bool IsAscending { get; set; } = true;
} 