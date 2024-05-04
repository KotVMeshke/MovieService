using MovieApi.DBContext;
using MovieApi.Services.Interface;

namespace MovieApi.Services
{
    public class FeedbackService(MovieServiceContext dbContext) : IFeedbackService
    {
    }
}
