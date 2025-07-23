using MediatR;
using MinimalAirbnb.Application.Messages.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model.Pagination;

namespace MinimalAirbnb.Application.Messages.Queries.GetMessages;

/// <summary>
/// Messages listesi query
/// </summary>
public class GetMessagesQuery : IRequest<Result<PagedList<MessageDto>>>
{
    /// <summary>
    /// Gönderen kullanıcı ID filtresi
    /// </summary>
    public Guid? SenderId { get; set; }
    
    /// <summary>
    /// Alıcı kullanıcı ID filtresi
    /// </summary>
    public Guid? ReceiverId { get; set; }
    
    /// <summary>
    /// Okundu mu filtresi
    /// </summary>
    public bool? IsRead { get; set; }
    
    /// <summary>
    /// Sayfa numarası
    /// </summary>
    public int PageNumber { get; set; } = 1;
    
    /// <summary>
    /// Sayfa boyutu
    /// </summary>
    public int PageSize { get; set; } = 10;
} 