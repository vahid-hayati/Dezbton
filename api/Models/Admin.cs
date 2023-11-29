// namespace api.Models;

// public record Admin(
//     [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
//     [MinLength(3), MaxLength(10)] string UserNeme,
//     [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$")] string Email,
//     [MinLength(8)] string Password,
//     [MinLength(8)] string? ConfirmPassword  
// );