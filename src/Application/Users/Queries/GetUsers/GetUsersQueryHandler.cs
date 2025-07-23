using MediatR;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Users.Queries.GetUsers;
using MinimalAirbnb.Application.Users.DTOs;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Application.Users.Queries.GetUsers;

/// <summary>
/// Users listesi query handler'ı
/// </summary>
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedList<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PagedList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _userRepository.GetAll();

            // Search filter
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(u => 
                    u.FirstName.Contains(request.SearchTerm) ||
                    u.LastName.Contains(request.SearchTerm) ||
                    u.Email.Contains(request.SearchTerm) ||
                    u.PhoneNumber.Contains(request.SearchTerm)
                );
            }

            // Sorting
            query = request.SortBy?.ToLower() switch
            {
                "firstname" => request.IsAscending ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName),
                "lastname" => request.IsAscending ? query.OrderBy(u => u.LastName) : query.OrderByDescending(u => u.LastName),
                "email" => request.IsAscending ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email),
                "createdat" => request.IsAscending ? query.OrderBy(u => u.Id) : query.OrderByDescending(u => u.Id),
                _ => query.OrderByDescending(u => u.Id)
            };

            // Get paged list
            var pagedList = await query.ToPagedListAsync(request.PageNumber - 1, request.PageSize);

            // Map to DTOs
            var userDtos = pagedList.Data.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                DateOfBirth = u.DateOfBirth
            }).ToList();

            // Create new PagedList with DTOs
            return new PagedList<UserDto>(
                userDtos,
                request.PageNumber - 1,
                request.PageSize,
                pagedList.TotalCount
            );
        }
        catch (Exception ex)
        {
            return new PagedList<UserDto>(
                new List<UserDto>(),
                request.PageNumber - 1,
                request.PageSize,
                0
            );
        }
    }
}
