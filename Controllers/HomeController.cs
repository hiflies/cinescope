using Microsoft.AspNetCore.Mvc;

namespace CineScope.Controllers;

public class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/not-found")]
    public IActionResult NotFoundPage()
    {
        return View();
    }
}
