using Microsoft.AspNetCore.Mvc;

namespace CineScope.Controllers;

public class SearchController : Controller
{
    [HttpGet("/search")]
    public IActionResult Index()
    {
        return View();
    }
}
