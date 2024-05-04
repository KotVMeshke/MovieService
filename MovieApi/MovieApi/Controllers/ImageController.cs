using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
