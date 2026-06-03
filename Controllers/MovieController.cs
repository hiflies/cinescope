using CineScope.Services;
using CineScope.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CineScope.Controllers;

public class MovieController(MovieRepository repository) : Controller
{
    [HttpGet("/movies/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var movie = await repository.Get(id);
        if (movie == null) return NotFound();

        ViewBag.Title = movie.Title;
        ViewBag.Tagline = movie.Tagline;
        ViewBag.ReleaseYear = movie.ReleaseYear.ToString();
        ViewBag.Duration = movie.Duration.ToTimeString();
        ViewBag.AgeRating = movie.AgeRating;
        ViewBag.Description = movie.Description;
        ViewBag.Director = movie.Director;
        ViewBag.Actors = movie.Actors;
        ViewBag.PosterUrl = movie.PosterUrl;
        ViewBag.BackdropImageUrl = movie.BackdropImageUrl;
        ViewBag.Genres = string.Join(" • ", movie.Genres.Select(g => g.Name));
        ViewBag.Rating = movie.Rating.ToString("N1");

        return View();
    }
}
