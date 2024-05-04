using Microsoft.AspNetCore.Mvc;
using MovieApi.Services.Interface;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/v1.0/feedback")]
    public class FeedbackController(IFeedbackService feedbackService,ILogger<FeedbackController> logger) : Controller
    {
    }
}
