namespace api.Models;

public record FeedBack(
 [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
 [MinLength(11), MaxLength(13)] string PhoneNumber,
 [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$")] string? Email,
 string Comments
);