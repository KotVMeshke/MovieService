using Microsoft.AspNetCore.Mvc;
using MovieApi.DTO;
using MovieApi.Services.Interface;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/v1.0/movie")]
    public class MovieController(IMovieService movieService,ILogger<MovieController> logger) : Controller
    {
        [HttpGet]
        public async Task<IResult> GetMovies(int offset, int limit)
        {
            var result = await movieService.GetFilms(offset, limit);
            if (result != null)
            {
                logger.LogInformation($"Films were getted succesfuly");

                var response = new MSRespone(StatusCodes.Status302Found, "Film were geted", result);
                Response.StatusCode = StatusCodes.Status302Found;
                return TypedResults.Json(response);
            }
            else
            {

                logger.LogInformation("Error during films getting");

                var responce = new MSRespone(StatusCodes.Status400BadRequest, "Error during films getting", result);
                Response.StatusCode = StatusCodes.Status400BadRequest;

                return TypedResults.Json(responce);
            }
        }

    }
}
