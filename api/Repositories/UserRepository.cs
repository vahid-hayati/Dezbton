namespace api.Repositories;

public class UserRepository : IUserRepository
{
    #region Constructor Section
    private const string _collectionName = "users";
    private readonly IMongoCollection<Register>? _collection;

    public UserRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<Register>(_collectionName);
    }
    #endregion Constructor Section

    public async Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Register> allUsers = await _collection.Find<Register>(new BsonDocument()).ToListAsync(cancellationToken);

        List<UserDto> userDtos = new List<UserDto>();

        if (allUsers.Any())
        {
            foreach (Register userIn in allUsers)
            {
                UserDto userDto = new UserDto(
                    Id: userIn.Id!,
                    UserName: userIn.UserName
                );

                userDtos.Add(userDto);
            }

            return userDtos;
        }

        return userDtos; // anyway, it returns an empty list of userDtos
    }

    public async Task<UserDto?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        Register docs = await _collection.Find<Register>(doc =>
        doc.PhoneNumber == phoneNumber).FirstOrDefaultAsync(cancellationToken);

        if (docs is not null)
            return new UserDto(
                Id: docs.Id!,
                UserName: docs.UserName
            );

        return null;
    }

    public async Task<UpdateResult> UpdateByUserIdAsync(string userId, Register userIn)
    {
        var updateResult = Builders<Register>.Update
        .Set((Register doc) => doc.FirstName, userIn.FirstName.ToLower().Trim())
        .Set(doc => doc.LastName, userIn.LastName.ToLower().Trim())
        .Set(doc => doc.UserName, userIn.UserName.ToLower().Trim())
        .Set(doc => doc.PhoneNumber, userIn.PhoneNumber.ToLower().Trim())
        .Set(doc => doc.Email, userIn.Email?.ToLower().Trim());
        // .Set(doc => doc.Password, userIn.Password.ToLower().Trim())
        // .Set(doc => doc.ConfirmPassword, userIn.ConfirmPassword.ToLower().Trim());

        return await _collection.UpdateOneAsync<Register>(doc => doc.Id == userId, updateResult);
    }

    public async Task<DeleteResult> DeleteAsync(string userId)
    {
        return await _collection.DeleteOneAsync<Register>(doc => doc.Id == userId);
    }
}