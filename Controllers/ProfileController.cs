using Microsoft.AspNetCore.Mvc;

namespace CineScope.Controllers;

public class ProfileController : Controller
{
    [HttpGet("/profile")]
    public IActionResult Index()
    {
        return View();
    }
}
