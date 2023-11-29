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
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="فرمت ایمیل درست نیست")] string? Email,
    //Password
    [DataType(DataType.Password), MinLength(8), MaxLength(16)] string Password,
    //ConfirmPassword
    [DataType(DataType.Password), MinLength(8), MaxLength(16)] string ConfirmPassword
);

public record LoginDto(
    [MinLength(3), MaxLength(10)]
    string UserName,
    
    [DataType(DataType.Password), MinLength(8), MaxLength(16)]
    string Password
);

public record LoggedInDto(
    string Id,
    string UserName,
    string Token
);