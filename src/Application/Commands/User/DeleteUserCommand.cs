using MediatR;

namespace MinimalAirbnb.Application.Commands.User;

/// <summary>
/// Kullanıcı Silme Komutu
/// </summary>
public class DeleteUserCommand : IRequest<bool>
{
    /// <summary>
    /// Kullanıcı ID
    /// </summary>
    public Guid Id { get; set; }
} 