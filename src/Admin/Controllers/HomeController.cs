using Microsoft.AspNetCore.Mvc;

namespace MinimalAirbnb.Admin.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
