using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApi.DTO;
using MovieApi.Models;
using MovieApi.Services;
using MovieApi.Services.Interface;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/v1.0/crew")]
    public class CrewController(ICrewService crewService ,ILogger<CrewController> logger) : Controller
    {
        [HttpGet("{crewId:int}")]
        public async Task<IResult> GetCrewMember([FromRoute] int crewId)
        {
            var result = await crewService.GetCrewById(crewId);
            if (result != null)
            {
                logger.LogInformation($"Crew member {crewId} was getted succesfuly");

                var response = new MSRespone(StatusCodes.Status302Found, $"Crew {crewId} was getted", result);
                Response.StatusCode = StatusCodes.Status302Found;
                return TypedResults.Json(response);
            }
            else
            {
                logger.LogError($"Error during crew member {crewId} getting");

                var responce = new MSRespone(StatusCodes.Status400BadRequest, $"Error during crew member {crewId} getting", result);
                Response.StatusCode = StatusCodes.Status400BadRequest;

                return TypedResults.Json(responce);
            }
        }
    }
}
