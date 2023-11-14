using System.Security.Cryptography;
using System.Text;

namespace api.Repositories;

public class UserRepository : IUserRepository
{
    private const string _collectionName = "users";
    private readonly IMongoCollection<Register>? _collection;

    public UserRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<Register>(_collectionName);
    }

    public async Task<UserDto?> CreateAsync(RegisterDto userInput, CancellationToken cancellationToken)
    {
        bool formDocs = await _collection.Find<Register>(doc =>
           doc.PhoneNumber == userInput.PhoneNumber.Trim()).AnyAsync(cancellationToken);

        if (formDocs)
            return null;

        // Register fromDoc = new Register(
        //     Id: null,
        //     FirstName: userInput.FirstName.Trim(),
        //     LastName: userInput.LastName.Trim(),
        //     UserName: userInput.UserName.ToLower().Trim(),
        //     PhoneNumber: userInput.PhoneNumber.Trim(),
        //     Email: userInput.Email?.ToLower().Trim(),
        //     Password: userInput.Password.Trim(),
        //     ConfirmPassword: userInput.ConfirmPassword.Trim()
        // );

        using var hmac = new HMACSHA512();

        Register fromDoc = new Register(
            Id: null,
            FirstName: userInput.FirstName.Trim(),
            LastName: userInput.LastName.Trim(),
            UserName: userInput.UserName.ToLower().Trim(),
            PhoneNumber: userInput.PhoneNumber.Trim(),
            Email: userInput.Email?.ToLower().Trim(),
            PasswordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.Password)),
            PasswordSalt: hmac.Key
        );

        if (_collection is not null)
            await _collection.InsertOneAsync(fromDoc, null, cancellationToken);

        if (fromDoc.Id is not null)
        {
            return new UserDto(
             Id: fromDoc.Id,
             userName: fromDoc.UserName
            );
            // UserDto userDto = new UserDto(
            //     Id: fromDoc.Id,
            //     userName: fromDoc.UserName
            // );

            // return userDto;
        }

        return null;
    }

    public async Task<UserDto?> LoginAsync(LoginDto userInput, CancellationToken cancellationToken)
    {
        Register login = await _collection.Find<Register>(doc =>
        doc.UserName == userInput.UserName.ToLower().Trim()).FirstOrDefaultAsync(cancellationToken);

        using var hmac = new HMACSHA512(login.PasswordSalt!);

        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.Password));

        if (login.PasswordHash is not null && login.PasswordHash.SequenceEqual(ComputeHash))
        {
            if (login.Id is not null)
            {
                return new UserDto(
                    Id: login.Id,
                    userName: login.UserName
                );
            }
        }

        return null;
    }

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
                    userName: userIn.UserName
                );

                userDtos.Add(userDto);
            }

            return userDtos;
        }

        return userDtos; // anyway, it returns an empty list of userDtos
    }

    public async Task<Register?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        Register docs = await _collection.Find<Register>(doc =>
        doc.PhoneNumber == phoneNumber).FirstOrDefaultAsync(cancellationToken);

        if (docs is null)
            return null;

        return docs;
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