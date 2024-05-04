using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controllers
{
    public class VideoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
