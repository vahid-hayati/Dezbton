namespace api.DTOs;

public record FeedbackDto(
    [MinLength(3), MaxLength(30)] string Name,
    [MinLength(3), MaxLength(300)] string Comments
);