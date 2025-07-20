using MediatR;
using MinimalAirbnb.Application.DTOs.User;

namespace MinimalAirbnb.Application.Queries.User;

/// <summary>
/// Email'e Göre Kullanıcı Getirme Sorgusu
/// </summary>
public class GetUserByEmailQuery : IRequest<UserResultDto?>
{
    /// <summary>
    /// Email adresi
    /// </summary>
    public string Email { get; set; } = string.Empty;
} 