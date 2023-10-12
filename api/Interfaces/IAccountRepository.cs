namespace api.Interfaces;

public interface IAccountRepository
{
    public Task<UserDto?> Create(RegisterDto userInput, CancellationToken cancellationToken);

    public Task<UserDto?> Login(LoginDto userInput, CancellationToken cancellationToken);

    public Task<Register?> GetByPhoneNumber(CallDto userIn, CancellationToken cancellationToken);

    public Task<UpdateResult> UpdateByUserId(string userId, Register userIn);

    public Task<DeleteResult> Delete(string userId);
}