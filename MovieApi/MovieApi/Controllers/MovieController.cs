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

                logger.LogError("Error during films getting");

                var responce = new MSRespone(StatusCodes.Status400BadRequest, "Error during films getting", result);
                Response.StatusCode = StatusCodes.Status400BadRequest;

                return TypedResults.Json(responce);
            }
        }

        [HttpGet]
        [Route("{filmId:int}")]
        public async Task<IResult> GetMovie([FromRoute] int filmId)
        {
            var result = await movieService.GetFilmById(filmId);
            if (result != null)
            {
                logger.LogInformation($"Film {filmId} was getted succesfuly");

                var response = new MSRespone(StatusCodes.Status302Found, $"Film {filmId} was geted", result);
                Response.StatusCode = StatusCodes.Status302Found;
                return TypedResults.Json(response);
            }
            else
            {

                logger.LogError($"Error during film {filmId} getting");

                var responce = new MSRespone(StatusCodes.Status400BadRequest, $"Error during film {filmId} getting", result);
                Response.StatusCode = StatusCodes.Status400BadRequest;

                return TypedResults.Json(responce);
            }
        }

    }
}
