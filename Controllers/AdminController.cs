using CineScope.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineScope.Controllers;

[Authorize(Roles = "admin")]
public class AdminController(MovieRepository repository, UserRepository userRepository) : Controller
{
    [HttpGet("/admin")]
    public IActionResult Index()
    {
        var totalMovies = repository.CreateQuery().Count();
        var totalUsers = userRepository.CreateQuery().Count();
        var totalReviews = 0;
        var topRatedMovie = repository.CreateQuery().OrderByDescending(r => r.Rating).First();
        
        ViewBag.TotalMovies = totalMovies;
        ViewBag.TotalUsers = totalUsers;
        ViewBag.TotalReviews = totalReviews;
        ViewBag.TopRatedMovie = topRatedMovie;
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
