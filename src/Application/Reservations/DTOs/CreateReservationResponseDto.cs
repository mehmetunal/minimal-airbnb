namespace MinimalAirbnb.Application.Reservations.DTOs;

/// <summary>
/// Rezervasyon oluşturma response DTO'su
/// </summary>
public class CreateReservationResponseDto
{
    public Guid ReservationId { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
} 