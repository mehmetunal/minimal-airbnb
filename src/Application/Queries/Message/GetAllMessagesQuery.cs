using MediatR;
using MinimalAirbnb.Application.DTOs.Message;

namespace MinimalAirbnb.Application.Queries.Message;

/// <summary>
/// Tüm Mesajları Getirme Sorgusu
/// </summary>
public class GetAllMessagesQuery : IRequest<IEnumerable<MessageListDto>>
{
    /// <summary>
    /// Sayfa numarası
    /// </summary>
    public int PageNumber { get; set; } = 1;
    
    /// <summary>
    /// Sayfa boyutu
    /// </summary>
    public int PageSize { get; set; } = 10;
    
    /// <summary>
    /// Gönderen kullanıcı ID filtresi
    /// </summary>
    public Guid? SenderId { get; set; }
    
    /// <summary>
    /// Alıcı kullanıcı ID filtresi
    /// </summary>
    public Guid? ReceiverId { get; set; }
    
    /// <summary>
    /// Rezervasyon ID filtresi
    /// </summary>
    public Guid? ReservationId { get; set; }
    
    /// <summary>
    /// Ev ID filtresi
    /// </summary>
    public Guid? PropertyId { get; set; }
    
    /// <summary>
    /// Okunmamış mesajlar
    /// </summary>
    public bool? UnreadOnly { get; set; }
} 