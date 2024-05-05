using Microsoft.AspNetCore.Mvc;
using MovieApi.DTO;
using MovieApi.Services;
using MovieApi.Services.Interface;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/v1.0/library")]
    public class LibraryController(ILibraryService libraryService, ILogger<LibraryController> logger) : Controller
    {
        [HttpPost]
        [Route("add")]
        public async Task<IResult> AddFilmIntoLibrary(int userId, int filmId)
        {
            var result = await libraryService.AddFilmIntoLibrary(userId, filmId);
            if (result)
            {
                logger.LogInformation($"User {userId} added film {filmId} succesfuly");
                Response.StatusCode = StatusCodes.Status201Created;
                var response = new MSRespone(StatusCodes.Status201Created, $"User {userId} added film {filmId} succesfuly", result);
                return TypedResults.Json(response);
            }
            else
            {
                logger.LogError("Error during adding film into library");
                Response.StatusCode = StatusCodes.Status400BadRequest;
                var responce = new MSRespone(StatusCodes.Status400BadRequest, "Error during adding film into library", result);

                return TypedResults.Json(responce);
            }
        }

        [HttpDelete]
        [Route("remove")]
        public async Task<IResult> RemoveFilmFromLibrary(int userId, int filmId)
        {
            var result = await libraryService.RemoveFilmFromLibrary(userId, filmId);
            if (result)
            {
                logger.LogInformation($"User {userId} removed a film {filmId} succesfuly");
                Response.StatusCode = StatusCodes.Status200OK;
                var response = new MSRespone(StatusCodes.Status302Found, $"User {userId} removed film {filmId} succesfuly", result);
                return TypedResults.Json(response);
            }
            else
            {
                logger.LogError("Error during removing film from library");

                var responce = new MSRespone(StatusCodes.Status400BadRequest, "Error during removing film from library", result);
                return TypedResults.Json(responce);
            }
        }

        [HttpGet]
        [Route("{userId:int}")]
        public async Task<IResult> GetLibrary([FromRoute] int userId)
        {
            var result = await libraryService.GetLibrary(userId);
            if (result != null)
            {
                logger.LogInformation($"User {userId} getted films succesfuly");
                Response.StatusCode = StatusCodes.Status302Found;
                var response = new MSRespone(StatusCodes.Status302Found, $"User {userId} getted films succesfuly", result);
                return TypedResults.Json(response);
            }
            else
            {
                logger.LogError("Error during getting library");
                Response.StatusCode = StatusCodes.Status400BadRequest;
                var responce = new MSRespone(StatusCodes.Status400BadRequest, "Error during getting library", result);
                return TypedResults.Json(responce);
            }
        }
    }
}
