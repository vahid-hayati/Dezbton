namespace api.Models;

public record Register(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
     string FirstName,
     string LastName,
     string UserName,
     string PhoneNumber,
     string? Email,
     string Password,
     string ConfirmPassword
);