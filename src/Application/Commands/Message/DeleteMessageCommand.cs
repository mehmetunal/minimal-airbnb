using MediatR;

namespace MinimalAirbnb.Application.Commands.Message;

/// <summary>
/// Mesaj Silme Komutu
/// </summary>
public class DeleteMessageCommand : IRequest<bool>
{
    /// <summary>
    /// Mesaj ID
    /// </summary>
    public Guid Id { get; set; }
} 