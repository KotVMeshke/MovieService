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
        [Route("genre/{filmId:int}")]
        public async Task<IResult>GetMovieGenres([FromRoute] int filmId)
        {
            var result = await movieService.GetGenresForFilm(filmId);
            if (result != null)
            {
                logger.LogInformation($"Genres for film {filmId} was getted succesfuly");

                var response = new MSRespone(StatusCodes.Status302Found, $"Genres for film {filmId} was getted", result);
                Response.StatusCode = StatusCodes.Status302Found;
                return TypedResults.Json(response);
            }
            else
            {

                logger.LogError($"Error during Genres for film {filmId} getting");

                var responce = new MSRespone(StatusCodes.Status400BadRequest, $"Error during Genres for film {filmId} getting", result);
                Response.StatusCode = StatusCodes.Status400BadRequest;

                return TypedResults.Json(responce);
            }

        }

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

                var response = new MSRespone(StatusCodes.Status302Found, $"Film {filmId} was getted", result);
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

        [HttpGet]
        [Route("crew/{filmId:int}")]
        public async Task<IResult> GetFilmCrew([FromRoute] int filmId)
        {
            var result = await movieService.GetCrewForFilm(filmId);
            if (result != null)
            {
                logger.LogInformation($"Crew for film {filmId} was getted succesfuly");

                var response = new MSRespone(StatusCodes.Status302Found, $"Crew for film {filmId} was getted", result);
                Response.StatusCode = StatusCodes.Status302Found;
                return TypedResults.Json(response);
            }
            else
            {

                logger.LogError($"Error during crew for film {filmId} getting");

                var responce = new MSRespone(StatusCodes.Status400BadRequest, $"Error during crew for film {filmId} getting", result);
                Response.StatusCode = StatusCodes.Status400BadRequest;

                return TypedResults.Json(responce);
            }
        }

    }
}
