using Microsoft.AspNetCore.Mvc;

namespace CineScope.Controllers;

public class AccountController : Controller
{
    [HttpGet("/login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet("/create-account")]
    public IActionResult CreateAccount()
    {
        return View();
    }
}