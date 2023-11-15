namespace api.Controllers;

public class UserController : BaseApiController
{
    private readonly IUserRepository _userRepository;

    #region  dependency injection in the constructor
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    #endregion dependency injection in the constructor

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
    [HttpGet("get-by-phoneNumber/{Phonenumber}")]
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
    [HttpPut("update-by-userId/{userId}")]
    public async Task<ActionResult<UpdateResult>> UpdateByUserID(string userId, Register userIn)
    {
        UpdateResult? docs = await _userRepository.UpdateByUserIdAsync(userId, userIn);

        return docs;
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>void</returns>
    [HttpDelete("delete-by-userId/{userId}")]
    public async Task<DeleteResult> Delete(string userId)
    {
        return await _userRepository.DeleteAsync(userId);
    }
}