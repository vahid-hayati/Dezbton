namespace api.Interfaces;

public interface IFeedbackRepository
{
    public Task<FeedbackDto?> AddCommentAsync(Feedback userComment, CancellationToken cancellationToken);

    public Task<List<FeedbackDto>> GetAllAsync(CancellationToken cancellationToken);

    public Task<List<FeedbackDto>> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);

    public Task<DeleteResult> DeleteAsync(string feedbackId);
}