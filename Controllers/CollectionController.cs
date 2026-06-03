using Microsoft.AspNetCore.Mvc;

namespace CineScope.Controllers;

public class CollectionController : Controller
{
    [HttpGet("/my-collections")]
    public IActionResult Index()
    {
        return View();
    }
}
