using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    public class LibraryAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
