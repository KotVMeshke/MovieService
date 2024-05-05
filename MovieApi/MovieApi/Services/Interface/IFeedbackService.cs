using MovieApi.DTO.ResponseTO;

namespace MovieApi.Services.Interface
{
    public interface IFeedbackService
    {
        public Task<bool> AddFeedback(int userId, int filmId, int mark, string text);
        public Task<FeedbackResponseTo[]> GetFeedbacks(int filmId);
    }
}
