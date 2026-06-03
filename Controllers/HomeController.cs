using CineScope.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineScope.Controllers;

public class HomeController(MovieRepository repository) : Controller
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        var featuredMovies = repository.GetAll()
            .Include(m => m.Genres)
            .OrderByDescending(m => m.Rating)
            .Take(5)
            .ToList();
        
        ViewBag.FeaturedMovies = featuredMovies;
        return View();
    }

    [HttpGet("/not-found")]
    public IActionResult NotFoundPage()
    {
        return View();
    }
}
