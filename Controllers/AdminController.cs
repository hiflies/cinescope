using CineScope.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        var recentlyAddedMovies = repository.CreateQuery()
            .Include(m => m.Genres)
            .OrderByDescending(m => m.Id)
            .Take(5)
            .ToList();

        ViewBag.TotalMovies = totalMovies;
        ViewBag.TotalUsers = totalUsers;
        ViewBag.TotalReviews = totalReviews;
        ViewBag.TopRatedMovie = topRatedMovie;
        ViewBag.RecentlyAddedMovies = recentlyAddedMovies;
        return View();
    }
}