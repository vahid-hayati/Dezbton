namespace api.Interfaces;

public interface IAccountRepository
{
    public Task<UserDto?> CreateAsync(RegisterDto userInput, CancellationToken cancellationToken);

    public Task<UserDto?> LoginAsync(LoginDto userInput, CancellationToken cancellationToken);
}