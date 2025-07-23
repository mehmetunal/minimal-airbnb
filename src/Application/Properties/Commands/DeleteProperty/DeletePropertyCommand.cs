using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Properties.Commands.DeleteProperty;

/// <summary>
/// Property silme command'i
/// </summary>
public class DeletePropertyCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
} 