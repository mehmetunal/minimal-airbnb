using MediatR;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Messages.Commands.UpdateMessage;

/// <summary>
/// Mesaj güncelleme komutu
/// </summary>
public class UpdateMessageCommand : IRequest<Result<object>>
{
    public Guid Id { get; set; }
    public string? Subject { get; set; }
    public string? Content { get; set; }
    public bool? IsRead { get; set; }
    public bool? IsArchived { get; set; }
} 