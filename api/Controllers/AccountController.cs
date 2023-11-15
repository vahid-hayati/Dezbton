namespace api.Controllers;

public class AccountController : BaseApiController
{
    private readonly IAccountRepository _accountRepository;

    #region dependency injection in the constructor
    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
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

        UserDto? userDto = await _accountRepository.CreateAsync(userInput, cancellationToken);

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
        UserDto? userDto = await _accountRepository.LoginAsync(userInput, cancellationToken);

        if (userDto is null)
            return Unauthorized("نام کاربری یا رمز ورود اشتباه است");

        return userDto;
    }
}