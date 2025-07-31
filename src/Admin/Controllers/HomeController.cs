using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAirbnb.Admin.Controllers;

[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
