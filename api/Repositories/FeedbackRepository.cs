namespace api.Repositories;

public class FeedbackRepository : IFeedbackRepository
{
    private const string _collectionName = "feedback";

    private readonly IMongoCollection<Feedback>? _collection;

    public FeedbackRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<Feedback>(_collectionName);
    }

    public async Task<FeedbackDto?> AddCommentAsync(Feedback userComment, CancellationToken cancellationToken)
    {
        Feedback feedBack = new Feedback(
           Id: null,
           Name: userComment.Name.Trim(),
           PhoneNumber: userComment.PhoneNumber.Trim(),
           Email: userComment.Email?.ToLower().Trim(),
           Comments: userComment.Comments.Trim()
       );

        if (_collection is not null)
            await _collection.InsertOneAsync(feedBack, null, cancellationToken);

        if (feedBack.Id is not null)
        {
            return new FeedbackDto(
                Name: feedBack.Name,
                Comments: feedBack.Comments
            );
        }

        return null;

        // Method-old
        // bool formDocs = _collection.AsQueryable().Where<FeedBack>(doc =>
        // doc.PhoneNumber == userComments.PhoneNumber.Trim()).Any();

        // if (formDocs)
        // return BadRequest("شما با این شماره تماس ");

        // Feedback feedBack = new Feedback(
        //     Id: null,
        //     Name: userComment.Name.Trim(),
        //     PhoneNumber: userComment.PhoneNumber.Trim(),
        //     Email: userComment.Email?.ToLower().Trim(),
        //     Comments: userComment.Comments.Trim()
        // );

        // _collection.InsertOne(feedBack);

        // return feedBack;
    }

    public async Task<List<FeedbackDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Feedback> allComments = await _collection.Find<Feedback>(new BsonDocument()).ToListAsync(cancellationToken);

        List<FeedbackDto> feedbackDtos = new List<FeedbackDto>();

        if (allComments.Any())
        {
            foreach (Feedback userComment in allComments)
            {
                FeedbackDto feedbackDto = new FeedbackDto(
                    Name: userComment.Name,
                    Comments: userComment.Comments
                );

                feedbackDtos.Add(feedbackDto);
            }

            return feedbackDtos;
        }

        return feedbackDtos;
    }

    public async Task<List<FeedbackDto>> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        List<Feedback> docs = await _collection.Find<Feedback>(doc =>
        doc.PhoneNumber == phoneNumber).ToListAsync(cancellationToken);

        List<FeedbackDto> feedbackDtos = new List<FeedbackDto>();

        if (docs.Any())
        {
            foreach (Feedback userComment in docs)
            {
                FeedbackDto feedbackDto = new FeedbackDto(
                    Name: userComment.Name,
                    Comments: userComment.Comments
                );

                feedbackDtos.Add(feedbackDto);
            }

            return feedbackDtos;
        }

        return feedbackDtos;
    }

    public async Task<DeleteResult> DeleteAsync(string feedbackId)
    {
        return await _collection.DeleteOneAsync<Feedback>(doc => doc.Id == feedbackId);
    }
}