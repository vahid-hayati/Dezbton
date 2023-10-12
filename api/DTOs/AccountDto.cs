namespace api.DTOs;

public record RegisterDto(
    //FirstName
    [MinLength(3), MaxLength(30)] string FirstName,
   //LastName
    [MinLength(3), MaxLength(30)] string LastName,
    //UserName
    [MinLength(3), MaxLength(10)] string UserName,
    //PhoneNumber
    [MinLength(11), MaxLength(13)] string PhoneNumber,
    //Email
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$")] string? Email,
    //Password
    [DataType(DataType.Password), MinLength(8), MaxLength(16)] string Password,
    //ConfirmPassword
    [DataType(DataType.Password), MinLength(8), MaxLength(16)] string ConfirmPassword
);

public record LoginDto(
    string UserName,
    string Password
);

public record CallDto(
    string PhoneNumber
);