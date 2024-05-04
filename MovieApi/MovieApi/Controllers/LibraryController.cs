using Microsoft.AspNetCore.Mvc;
using MovieApi.Services.Interface;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/1.0/library")]
    public class LibraryController(ILibraryService libraryService,ILogger<LibraryController> logger) : Controller
    {
    }
}
