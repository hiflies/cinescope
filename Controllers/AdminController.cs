using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineScope.Controllers;

[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    [HttpGet("/admin")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/admin/reviews")]
    public IActionResult Reviews()
    {
        return View();
    }

    [HttpGet("/admin/users")]
    public IActionResult Users()
    {
        return View();
    }
}
