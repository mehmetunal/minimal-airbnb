using MediatR;
using MinimalAirbnb.Application.DTOs.Message;
using MinimalAirbnb.Application.Interfaces;
using AutoMapper;
using Maggsoft.Core.Base;

namespace MinimalAirbnb.Application.Commands.Message;

/// <summary>
/// Mesaj Oluşturma Handler'ı
/// </summary>
public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Result<MessageResultDto>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public CreateMessageCommandHandler(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<Result<MessageResultDto>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Command'den entity'ye dönüştür
            var message = _mapper.Map<Domain.Entities.Message>(request);

            // Mesajı ekle
            await _messageRepository.AddAsync(message);
            await _messageRepository.SaveChangesAsync();

            // Entity'den DTO'ya dönüştür
            var messageResultDto = _mapper.Map<MessageResultDto>(message);

            return Result<MessageResultDto>.Success(messageResultDto);
        }
        catch (Exception ex)
        {
            return Result<MessageResultDto>.Failure(new Maggsoft.Core.Model.Error("Mesaj oluşturma hatası", $"Mesaj oluşturulurken hata oluştu: {ex.Message}"));
        }
    }
} 