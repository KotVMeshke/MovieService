using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApi.Services;
using MovieApi.Services.Interface;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/v1.0/video")]
    public class VideoController(IVideoService videoService, ILogger<VideoController> logger) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetVideo(string path)
        {
            var result = videoService.GetVideo(path);

            if (result != null)
            {
                logger.LogInformation($"Video from {path} was sended to client succesfuly");
                return result;
            }
            logger.LogError($"Video from {path} wasn't sended because wasn't found");
            return NotFound();
        }
    }
}
