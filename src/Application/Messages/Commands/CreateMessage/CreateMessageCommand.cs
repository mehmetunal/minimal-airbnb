using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Messages.Commands.CreateMessage;

/// <summary>
/// Message oluşturma command'i
/// </summary>
public class CreateMessageCommand : IRequest<Result<object>>
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string Content { get; set; } = string.Empty;
} 