namespace MinimalAirbnb.Application.Reviews.Commands.CreateReview;

/// <summary>
/// Review oluşturma response DTO'su
/// </summary>
public class CreateReviewResponseDto
{
    public Guid ReviewId { get; set; }
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string PropertyTitle { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
} 