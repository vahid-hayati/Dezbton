namespace api.Controllers;

public class UserController : BaseApiController
{
    private readonly IUserRepository _userRepository;

    #region dependency injection in the constructor
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    #endregion dependency injection in the constructor

    /// <summary>
    /// Create accounts
    /// </summary>
    /// <param name="userInput"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>UserDto</returns>
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Create(RegisterDto userInput, CancellationToken cancellationToken)
    {
        if (userInput.Password != userInput.ConfirmPassword)
            return BadRequest("کلمه های عبور یکسان نیست");

        UserDto? userDto = await _userRepository.CreateAsync(userInput, cancellationToken);

        if (userDto is null)
            return BadRequest("نام و شناسه کاربری گرفته شده");

        return userDto;
    }

    /// <summary>
    /// Login accounts
    /// </summary>
    /// <param name="userInput"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>UserDto</returns>
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto userInput, CancellationToken cancellationToken)
    {
        UserDto? userDto = await _userRepository.LoginAsync(userInput, cancellationToken);

        if (userDto is null)
            return Unauthorized("نام کاربری یا رمز ورود اشتباه است");

        return userDto;
    }

    /// <summary>
    /// Get List<UserDto> 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>IEnumerable<UserDto></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(CancellationToken cancellationToken)
    {
        List<UserDto> userDtos = await _userRepository.GetAllAsync(cancellationToken);

        if (!userDtos.Any())
            return NoContent();

        return userDtos;
    }

    /// <summary>
    /// Get by phone number
    /// </summary>
    /// <param name="userIn"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Register Model</returns>
    [HttpGet("get-by-PhoneNumber/{PhoneNumber}")]
    public async Task<ActionResult<Register>> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken)
    {
       Register? docs = await _userRepository.GetByPhoneNumberAsync(phoneNumber, cancellationToken);

        if (docs is null)
            return NotFound("حساب کاربری یافت نشد");

        return docs;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="userIn"></param>
    /// <returns>Update Result</returns>
    [HttpPut("update/{userId}")]
    public async Task<ActionResult<UpdateResult>> UpdateByUserId(string userId, Register userIn)
    {
        UpdateResult? docs = await _userRepository.UpdateByUserIdAsync(userId, userIn);

        return docs;
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>void</returns>
    [HttpDelete("delete/{userId}")]
    public async Task<DeleteResult> Delete(string userId)
    {
        return await _userRepository.DeleteAsync(userId);
    }

}

// [HttpGet("get-by-phoneNumber/{phoneNumber}")]
// public ActionResult<Register> GetByPhoneNumber(string phoneNumber)
// {
//     Register user = _collection.Find(doc => doc.PhoneNumber == phoneNumber.Trim()).FirstOrDefault();

//     if (user is null)
//         return NotFound("کاربری با این شماره تماس پیدا نشد");

//     return user;
// }


// [HttpGet]
// public async Task<IEnumerable<UserDto?>> GetAll(List<RegisterDto> userInput, CancellationToken cancellationToken)
// {
//     List<UserDto?> listUsers = await _accountRepository.GetAll(userInput, cancellationToken);

//     if (!listUsers.Any())
//         return Ok("لیست خالی است");
// }


// [HttpGet]
// public ActionResult<IEnumerable<Register>> GettAll()
// {
//     List<Register> users = _collection.Find<Register>(new BsonDocument()).ToList();

//     if (!users.Any())
//         return NoContent();

//     return users;
// }