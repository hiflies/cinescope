using CineScope.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineScope.Controllers;

public class HomeController(MovieRepository repository, GenreRepository genreRepository) : Controller
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        var featuredMovies = repository.CreateQuery()
            .Include(m => m.Genres)
            .OrderByDescending(m => m.Rating)
            .Take(5)
            .ToList();

        var recentlyAddedMovies = repository.CreateQuery()
            .OrderByDescending(m => m.Id)
            .Take(10)
            .ToList();

        var topRatedMovies = repository.CreateQuery()
            .OrderByDescending(m => m.Rating)
            .Take(10)
            .ToList();

        var genres = genreRepository.CreateQuery()
            .OrderByDescending(g => g.Movies.Count)
            .ToList();

        ViewBag.FeaturedMovies = featuredMovies;
        ViewBag.RecentlyAddedMovies = recentlyAddedMovies;
        ViewBag.TopRatedMovies = topRatedMovies;
        ViewBag.Genres = genres;
        return View();
    }

    [HttpGet("/not-found")]
    public IActionResult NotFoundPage()
    {
        return View();
    }
}