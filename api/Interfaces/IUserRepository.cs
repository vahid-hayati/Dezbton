namespace api.Interfaces;

public interface IUserRepository
{
    public Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken);

    public Task<UserDto?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);

    public Task<UpdateResult> UpdateByUserIdAsync(string userId, Register userIn);

    public Task<DeleteResult> DeleteAsync(string userId);
}