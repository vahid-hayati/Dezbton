namespace api.Controllers;

[Authorize]
public class FeedbackController : BaseApiController
{
    private readonly IFeedbackRepository _feedbackRepository;

    #region  dependency injection in the constructor
    public FeedbackController(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }
    #endregion dependency injection in the constructor

    /// <summary>
    /// Add Comments
    /// </summary>
    /// <param name="userComment"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>FeedbackDto</returns>
    [HttpPost("add_comments")]
    public async Task<ActionResult<FeedbackDto>> AddComment(Feedback userComment, CancellationToken cancellationToken)
    {
        FeedbackDto? feedbackDto = await _feedbackRepository.AddCommentAsync(userComment, cancellationToken);

        if (feedbackDto is null)
            return BadRequest("کامنت ثبت نشد");

        return feedbackDto;
    }

    /// <summary>
    /// Get all List<FeedbackDto> 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>IEnumerable<FeedbackDto></returns>
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeedbackDto>>> GetAll(CancellationToken cancellationToken)
    {
        List<FeedbackDto> feedbackDtos = await _feedbackRepository.GetAllAsync(cancellationToken);

        if (!feedbackDtos.Any())
            return NoContent();

        return feedbackDtos;
    }

    /// <summary>
    /// Get by phone number List<FeedbackDto> 
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>IEnumerable<FeedbackDto></returns>
    [HttpGet("get_by_phoneNumber/{phoneNumber}")]
    public async Task<ActionResult<IEnumerable<FeedbackDto>>> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken)
    {
        List<FeedbackDto> feedbackDtos = await _feedbackRepository.GetByPhoneNumberAsync(phoneNumber, cancellationToken);

        if (!feedbackDtos.Any())
            return NoContent();

        return feedbackDtos;
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="feedbackId"></param>
    /// <returns>void</returns>
    [HttpDelete("delete/{feedbackId}")]
    public async Task<DeleteResult> Delete(string feedbackId)
    {
        return await _feedbackRepository.DeleteAsync(feedbackId);
    }
}