namespace api.Models;

public record Feedback(
 [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
  string Name,
  string PhoneNumber,
 [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "فرمت ایمیل درست نیست")] string? Email,
 string Comments
);