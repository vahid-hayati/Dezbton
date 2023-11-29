namespace api.Controllers;

public class AccountController : BaseApiController
{
    #region Constructor Section
    private readonly IAccountRepository _accountRepository;

    // constructor - dependency injection
    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    #endregion Constructor Section

    /// <summary>
    /// Create accounts
    /// </summary>
    /// <param name="userInput"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>UserDto</returns>
    [HttpPost("register")]
    public async Task<ActionResult<LoggedInDto>> Create(RegisterDto userInput, CancellationToken cancellationToken)
    {
        if (userInput.Password != userInput.ConfirmPassword)
            return BadRequest("کلمه های عبور یکسان نیست");

        LoggedInDto? loggedInDto = await _accountRepository.CreateAsync(userInput, cancellationToken);

        if (loggedInDto is null)
            return BadRequest("شما با این مشخصات ثبت نام انجام داده اید!");

        return loggedInDto;
    }

    /// <summary>
    /// Login accounts
    /// </summary>
    /// <param name="userInput"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>UserDto</returns>
    [HttpPost("login")]
    public async Task<ActionResult<LoggedInDto>> Login(LoginDto userInput, CancellationToken cancellationToken)
    {
        LoggedInDto? loggedInDto = await _accountRepository.LoginAsync(userInput, cancellationToken);

        if (loggedInDto is null)
            return Unauthorized("نام کاربری یا رمز ورود اشتباه است");

        return loggedInDto;
    }
}