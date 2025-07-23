using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Application.Messages.Queries.GetMessages;
using MinimalAirbnb.Application.Messages.DTOs;
using Maggsoft.Core.Base;
using Maggsoft.Core.Model;
using Maggsoft.Core.Model.Pagination;
using Maggsoft.Core.Extensions;

namespace MinimalAirbnb.Application.Messages.Queries.GetMessages;

/// <summary>
/// Messages listesi query handler'ı
/// </summary>
public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, Result<PagedList<MessageDto>>>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessagesQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Result<PagedList<MessageDto>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _messageRepository.GetAll();

            // Filtreler
            if (request.SenderId.HasValue)
            {
                query = query.Where(m => m.SenderId == request.SenderId.Value);
            }

            if (request.ReceiverId.HasValue)
            {
                query = query.Where(m => m.ReceiverId == request.ReceiverId.Value);
            }

            if (request.IsRead.HasValue)
            {
                query = query.Where(m => m.IsRead == request.IsRead.Value);
            }

            // Sıralama
            query = query.OrderByDescending(m => m.CreatedDate);

            // Sayfalama
            var pagedResult = await query.ToPagedListAsync(request.PageNumber - 1, request.PageSize);

            if (!pagedResult.Data.Any())
            {
                var emptyResult = new PagedList<MessageDto>([], pagedResult.TotalCount, pagedResult.PageIndex + 1, pagedResult.PageSize);
                return Result<PagedList<MessageDto>>.Success(emptyResult, new SuccessMessage("200", "Sistemde mesaj bulunamadı."));
            }

            var messageDtos = pagedResult.Data.Select(m => new MessageDto
            {
                Id = m.Id,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Subject = m.Subject,
                Content = m.Content,
                ReservationId = m.ReservationId,
                PropertyId = m.PropertyId,
                IsRead = m.IsRead,
                ReadDate = m.ReadDate,
                ReplyToMessageId = m.ReplyToMessageId,
                MessageType = m.MessageType,
                Priority = m.Priority,
                Category = m.Category,
                AttachmentUrl = m.AttachmentUrl,
                CreatedAt = m.CreatedDate,
                UpdatedAt = m.ModifiedDate
            }).ToList();

            var result = new PagedList<MessageDto>(messageDtos, pagedResult.TotalCount, pagedResult.PageIndex + 1, pagedResult.PageSize);

            return Result<PagedList<MessageDto>>.Success(result, new SuccessMessage("200", "Mesajlar başarıyla getirildi."));
        }
        catch (Exception ex)
        {
            return Result<PagedList<MessageDto>>.Failure(new Error("500", $"Mesajlar getirilirken hata oluştu: {ex.Message}"));
        }
    }
} 