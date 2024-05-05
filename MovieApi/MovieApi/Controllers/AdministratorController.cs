using Microsoft.AspNetCore.Mvc;
using MovieApi.DTO;
using MovieApi.Services;
using MovieApi.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieApi.Controllers
{
    [Route("api/v1.0/admin")]
    [ApiController]
    public class AdministratorController(IAdminService adminService, ILogger<AdministratorController> logger) : ControllerBase
    {
        [HttpPut]
        [Route("ban")]
        public async Task<IResult> BanUser(int userId, int adminId)
        {
            var result = await adminService.BanUser(userId,adminId);
            if (result)
            {
                logger.LogInformation($"User {userId} was baned succesfuly");
                var response = new MSRespone(StatusCodes.Status302Found, $"User {userId} was baned succesfuly", result);
                return TypedResults.Json(response);
            }
            else
            {
                logger.LogError("Error during banning");

                var responce = new MSRespone(StatusCodes.Status400BadRequest, "Error during banning", result);
                return TypedResults.Json(responce);
            }
        }

        [HttpPut]
        [Route("unban")]
        public async Task<IResult> UnBanUser(int userId, int adminId)
        {
            var result = await adminService.UnBanUser(userId, adminId);
            if (result)
            {

                var response = new MSRespone(StatusCodes.Status302Found, $"User {userId} was unbaned succesfuly", result);
                return TypedResults.Json(response);
            }
            else
            {
                var responce = new MSRespone(StatusCodes.Status400BadRequest, "Error during banning", result);
                return TypedResults.Json(responce);
            }
        }
    }
}
