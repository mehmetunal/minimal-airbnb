using MediatR;
using MinimalAirbnb.Application.Messages.DTOs;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Messages.Queries.GetMessageById;

/// <summary>
/// Message by ID query
/// </summary>
public class GetMessageByIdQuery : IRequest<Result<MessageDto>>
{
    /// <summary>
    /// Mesaj ID
    /// </summary>
    public Guid Id { get; set; }
} 