namespace api.Models;

public record Feedback(
 [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
 [MinLength(3), MaxLength(30)] string Name,
 [MinLength(11), MaxLength(13)] string PhoneNumber,
 [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="فرمت ایمیل درست نیست")] string? Email,
 string Comments
);