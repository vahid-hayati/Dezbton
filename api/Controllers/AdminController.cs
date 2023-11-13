namespace api.Controllers;

public class AdminController : BaseApiController
{
    private readonly IMongoCollection<Admin> _collection;

    public AdminController(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<Admin>("admins");
    }

    [HttpPost("register")]
    public ActionResult<Admin> Create(Admin userAdmin)
    {
        if (userAdmin.Password != userAdmin.ConfirmPassword)
            return BadRequest(".کلمه عبور با تکرار کلمه عبور یکسان نیست");

        bool fromDocs = _collection.AsQueryable().Where<Admin>(doc =>
        doc.Email == userAdmin.Email && doc.Password == userAdmin.Password).Any();

        if (fromDocs)
            return BadRequest("ادمین با این مشخصات ثبت شده");

        Admin admin = new Admin(
            Id: null,
            UserNeme: userAdmin.UserNeme.ToLower().Trim(),
            Email: userAdmin.Email.ToLower().Trim(),
            Password: userAdmin.Password,
            ConfirmPassword: userAdmin?.ConfirmPassword
        );

        _collection.InsertOne(admin);

        return admin;
    }

    [HttpPost("login")]
    public ActionResult<Admin> Login(Admin adminIn)
    {
        Admin adminDoc = _collection.Find<Admin>(doc =>
        doc.Email == adminIn.Email.ToLower().Trim() && doc.Password == adminIn.Password).FirstOrDefault();

        if (adminDoc is null)
            return Unauthorized("شما ادمین نیستید");

        return adminDoc;
    }
}