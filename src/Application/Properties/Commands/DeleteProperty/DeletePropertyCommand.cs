using MediatR;
using MinimalAirbnb.Application.Common.Models;

namespace MinimalAirbnb.Application.Properties.Commands.DeleteProperty;

/// <summary>
/// Property silme command'i
/// </summary>
public class DeletePropertyCommand : IRequest<ApiResponse<bool>>
{
    /// <summary>
    /// Property ID'si
    /// </summary>
    public Guid Id { get; set; }
} 