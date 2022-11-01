using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
