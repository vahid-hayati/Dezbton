namespace api.Controllers;

public class FeedBackController : BaseApiController
{
    private readonly IMongoCollection<FeedBack> _collection;

    public FeedBackController(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<FeedBack>("feedBacks");
    }

    [HttpPost("register_comments")]
    public ActionResult<FeedBack> AddCooment(FeedBack userComments)
    {
        // bool formDocs = _collection.AsQueryable().Where<FeedBack>(doc =>
        // doc.PhoneNumber == userComments.PhoneNumber.Trim()).Any();

        // if (formDocs)
        // return BadRequest("شما با این شماره تماس ");

        FeedBack feedBack = new FeedBack(
            Id: null,
            PhoneNumber: userComments.PhoneNumber,
            Email: userComments.Email?.ToLower().Trim(),
            Comments: userComments.Comments
        );

        _collection.InsertOne(feedBack);

        return feedBack;
    }

    [HttpGet("get_by_phoneNumber/{phoneNumber}")]
    public ActionResult<IEnumerable<FeedBack>> GetByPhoneNumber(string phoneNumber)
    {
        List<FeedBack> comments = _collection.Find<FeedBack>(doc =>
        doc.Email == phoneNumber).ToList();

       if (comments is null)
            return NotFound("نظری با این شماره تماس ثبت نشده");

        return comments;
    }

    [HttpGet]
    public ActionResult<IEnumerable<FeedBack>> GettAll()
    {
        List<FeedBack> comments = _collection.Find<FeedBack>(new BsonDocument()).ToList();

        if (!comments.Any())
            return NoContent();

        return comments;
    }

    [HttpDelete("delete/{userId}")]
    public ActionResult<DeleteResult> Delete(string userId)
    {
        return _collection.DeleteOne<FeedBack>(doc => doc.Id == userId);
    }
}