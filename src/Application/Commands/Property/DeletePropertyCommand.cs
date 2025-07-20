using MediatR;

namespace MinimalAirbnb.Application.Commands.Property;

/// <summary>
/// Ev Silme Komutu
/// </summary>
public class DeletePropertyCommand : IRequest<bool>
{
    /// <summary>
    /// Ev ID
    /// </summary>
    public Guid Id { get; set; }
} 