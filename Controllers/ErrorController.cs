using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CineScope.Controllers;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class ErrorController : Controller
{
    [HttpGet("/Error")]
    public IActionResult Index()
    {
        ViewBag.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        ViewBag.ShowRequestId = !string.IsNullOrEmpty(ViewBag.RequestId);
        return View();
    }
}
