using MediatR;
using MinimalAirbnb.Application.DTOs.User;

namespace MinimalAirbnb.Application.Queries.User;

/// <summary>
/// ID'ye Göre Kullanıcı Getirme Sorgusu
/// </summary>
public class GetUserByIdQuery : IRequest<UserResultDto?>
{
    /// <summary>
    /// Kullanıcı ID
    /// </summary>
    public Guid Id { get; set; }
} 