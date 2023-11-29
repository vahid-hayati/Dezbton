namespace api.Repositories;

public class AccountRepository : IAccountRepository
{
    #region  Constructor Section
    private const string _collectionName = "users";
    private readonly IMongoCollection<Register>? _collection;

    private readonly ITokenService _tokenService;

    public AccountRepository(IMongoClient client, IMongoDbSettings dbSettings, ITokenService tokenService)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<Register>(_collectionName);
        _tokenService = tokenService;
    }
    #endregion Constructor Section

    public async Task<LoggedInDto?> CreateAsync(RegisterDto userInput, CancellationToken cancellationToken)
    {
        bool formDocs = await _collection.Find<Register>(doc =>
           doc.PhoneNumber == userInput.PhoneNumber.Trim()).AnyAsync(cancellationToken);

        if (formDocs)
            return null;

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
            return new LoggedInDto(
             Id: fromDoc.Id,
             UserName: fromDoc.UserName,
             Token: _tokenService.CreateToken(fromDoc)
            );
        }

        return null;
    }

    public async Task<LoggedInDto?> LoginAsync(LoginDto userInput, CancellationToken cancellationToken)
    {
        Register login = await _collection.Find<Register>(doc =>
        doc.UserName == userInput.UserName.ToLower().Trim()).FirstOrDefaultAsync(cancellationToken);

        if (login is null)
            return null;

        using var hmac = new HMACSHA512(login.PasswordSalt!);

        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.Password));

        if (login.PasswordHash is not null && login.PasswordHash.SequenceEqual(ComputeHash))
        {
            if (login.Id is not null)
            {
                return new LoggedInDto(
                    Id: login.Id,
                    UserName: login.UserName,
                    Token: _tokenService.CreateToken(login)
                );
            }
        }

        return null;
    }
}