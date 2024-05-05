using Microsoft.AspNetCore.Mvc;
using MovieApi.Services.Interface;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/v1.0/images")]
    public class ImageController(IImageService imageService, ILogger<LibraryController> logger) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetImage(string path)
        {
            var result = imageService.GetImage(path);

            if (result != null)
            {
                Response.ContentType = "image/webp";
                logger.LogInformation($"Image from {path} was sended to client succesfuly");
                return File(await result.ReadAsByteArrayAsync(), "image/webp");
            }
            logger.LogError($"Image from {path} wasn't sended because wasn't found");
            return NotFound();
        }
    }
}
