using Microsoft.AspNetCore.Mvc;
using MovieApi.DTO;
using MovieApi.Services;
using MovieApi.Services.Interface;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/v1.0/feedback")]
    public class FeedbackController(IFeedbackService feedbackService,ILogger<FeedbackController> logger) : Controller
    {
        [HttpPost]
        [Route("add")]
        public async Task<IResult> AddFeedback(int userId, int filmId,int mark, [FromBody] string text)
        {
            var result = await feedbackService.AddFeedback(userId,filmId,mark,text);
            if (result)
            {
                logger.LogInformation($"User {userId} added feedback with mark {mark} for film {filmId} succesfuly");
                Response.StatusCode = StatusCodes.Status201Created;
                var response = new MSRespone(StatusCodes.Status201Created, $"User {userId} added feedback with mark {mark} for film {filmId} succesfuly", result);
                return TypedResults.Json(response);
            }
            else
            {
                logger.LogError("Error during adding feedback");
                Response.StatusCode = StatusCodes.Status400BadRequest;
                var responce = new MSRespone(StatusCodes.Status400BadRequest, "Error during adding feedback", result);

                return TypedResults.Json(responce);
            }
        }

        [HttpGet]
        [Route("{filmId:int}")]
        public async Task<IResult> GetFeedBacks([FromRoute]int filmId)
        {
            var result =await feedbackService.GetFeedbacks(filmId);
            if (result != null)
            {
                logger.LogInformation($"Feedback for film {filmId} was getted succesfuly");

                var response = new MSRespone(StatusCodes.Status302Found, $"Feedback for film {filmId} was geted", result);
                Response.StatusCode = StatusCodes.Status302Found;
                return TypedResults.Json(response);
            }
            else
            {

                logger.LogError($"Error during feedbacks getting");

                var responce = new MSRespone(StatusCodes.Status400BadRequest, $"Error during feedbacks getting", result);
                Response.StatusCode = StatusCodes.Status400BadRequest;

                return TypedResults.Json(responce);
            }
        }
    }
}
