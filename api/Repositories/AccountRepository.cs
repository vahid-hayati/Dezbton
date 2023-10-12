using System.Security.Principal;

namespace api.Repositories;

public class AccountRepository : IAccountRepository
{
    private const string _collectionName = "users";
    private readonly IMongoCollection<Register>? _collection;

    public AccountRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<Register>(_collectionName);
    }

    /// <summary>
    /// Create accounts
    /// </summary>
    /// <param name="userInput"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>UserDto</returns>
    public async Task<UserDto?> Create(RegisterDto userInput, CancellationToken cancellationToken)
    {
        bool formDocs = await _collection.Find<Register>(doc =>
           doc.PhoneNumber == userInput.PhoneNumber.Trim()).AnyAsync(cancellationToken);

        if (formDocs)
            return null;

        Register fromDoc = new Register(
            Id: null,
            FirstName: userInput.FirstName.Trim(),
            LastName: userInput.LastName.Trim(),
            UserName: userInput.UserName.ToLower().Trim(),
            PhoneNumber: userInput.PhoneNumber.Trim(),
            Email: userInput.Email?.ToLower().Trim(),
            Password: userInput.Password.Trim(),
            ConfirmPassword: userInput.ConfirmPassword.Trim()
        );

        if (_collection is not null)
            await _collection.InsertOneAsync(fromDoc, null, cancellationToken);

        if (fromDoc.Id is not null)
        {
            UserDto userDto = new UserDto(
                Id: fromDoc.Id,
                userName: fromDoc.UserName
            );

            return userDto;
        }

        return null;
    }

    /// <summary>
    /// Login accounts
    /// </summary>
    /// <param name="userInput"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>UserDto</returns>
    public async Task<UserDto?> Login(LoginDto userInput, CancellationToken cancellationToken)
    {
        Register login = await _collection.Find<Register>(doc =>
        doc.UserName == userInput.UserName.ToLower().Trim() && doc.Password == userInput.Password.Trim()).FirstOrDefaultAsync(cancellationToken);

        if (login is null)
            return null;

        if (login.Id is not null)
        {
            UserDto userDto = new UserDto(
                Id: login.Id,
                userName: login.UserName
            );

            return userDto;
        }

        return null;
    }

    /// <summary>
    /// Get by phone number
    /// </summary>
    /// <param name="userIn"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Register Model</returns>
    public async Task<Register?> GetByPhoneNumber(CallDto userIn, CancellationToken cancellationToken)
    {
        Register docs = await _collection.Find<Register>(doc =>
        doc.PhoneNumber == userIn.PhoneNumber).FirstOrDefaultAsync(cancellationToken);

        if (docs is null)
            return null;

        return docs;
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="userIn"></param>
    /// <returns>Update Result</returns>
    public async Task<UpdateResult> UpdateByUserId(string userId, Register userIn)
    {
        var updateResult = Builders<Register>.Update
        .Set((Register doc) => doc.FirstName, userIn.FirstName.ToLower().Trim())
        .Set(doc => doc.LastName, userIn.LastName.ToLower().Trim())
        .Set(doc => doc.UserName, userIn.UserName.ToLower().Trim())
        .Set(doc => doc.PhoneNumber, userIn.PhoneNumber.ToLower().Trim())
        .Set(doc => doc.Email, userIn.Email?.ToLower().Trim())
        .Set(doc => doc.Password, userIn.Password.ToLower().Trim());

        return await _collection.UpdateOneAsync<Register>(doc => doc.Id == userId, updateResult);
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>void</returns>
    public async Task<DeleteResult> Delete(string userId)
    {
        return await _collection.DeleteOneAsync<Register>(doc => doc.Id == userId);
    }
}


// [HttpDelete("delete/{userId}")]
// public ActionResult<DeleteResult> Delete(string userId)
// {
//     return _collection.DeleteOne<Register>(doc => doc.Id == userId);
// }


// public async Task<List<UserDto?>> GetAll(List<RegisterDto> userInput, CancellationToken cancellationToken)
// {
//     bool users = await _collection.Find<Register>(new BsonDocument()).ToListAsync(cancellationToken);

//     if (!users.Any())
//         return null;


//     return users;

// }