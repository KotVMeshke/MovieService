using Microsoft.AspNetCore.Mvc;
using MovieApi.DBContext;
using MovieApi.DTO;
using MovieApi.Models;
using MovieApi.Services.Interface;
using System.IO;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/v1.0/user")]
    public class UserController(IUserService userService, ILogger<UserController> logger) : Controller
    {
        [HttpPost]
        [Route("registration")]
        public async Task<IResult> Registration(string email, string password, string name)
        {
            var result = await userService.RegistrateUser(email, password, name);
            if (result.UserID != -1)
            {
                logger.LogInformation($"User {result.UserID} was registrated succesfuly");
                Response.StatusCode = StatusCodes.Status201Created;
                var response = new MSRespone(StatusCodes.Status201Created, "User was created", result);
                return TypedResults.Json(response);
            }
            else
            {

                logger.LogError($"Error during user creation");

                Response.StatusCode = StatusCodes.Status400BadRequest;

                var responce = new MSRespone(StatusCodes.Status400BadRequest, "Error during user creation", result);
                return TypedResults.Json(responce);
            }
        }

        [HttpGet]
        [Route("authorization")]
        public async Task<IResult> Authorization(string email, string password)
        {
            var result = await userService.AuthorizeUser(email, password);
            if (result.UserID != -1)
            {
                logger.LogInformation("User was entered succesfuly");
                Response.StatusCode = StatusCodes.Status302Found;

                var response = new MSRespone(StatusCodes.Status302Found, "User was entered succesfuly", result);
                return TypedResults.Json(response);
            }
            else
            {
                logger.LogError($"Error during user authorization");

                Response.StatusCode = StatusCodes.Status400BadRequest;
                var response = new MSRespone(StatusCodes.Status400BadRequest, "Error during user authorization", result);
                return TypedResults.Json(response);
            }
        }
    }
}
