using CineScope.Models;
using CineScope.Services;
using CineScope.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineScope.Controllers;

public class MovieController(MovieRepository repository, GenreRepository genreRepository) : Controller
{
    [HttpGet("/movies")]
    public async Task<IActionResult> Index()
    {
        var movies = repository.CreateQuery()
            .Include(m => m.Genres)
            .OrderByDescending(m=>m.ReleaseYear)
            .ThenByDescending(m=>m.Id)
            .ToList();

        var genres = genreRepository.CreateQuery()
            .OrderByDescending(g => g.Movies.Count)
            .ToList();
        
        ViewBag.Movies = movies;
        ViewBag.Genres = genres;
        return View();
    }

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
        ViewBag.SimilarMovies = GetSimilarMovies(movie);

        return View();
    }

    private List<Movie> GetSimilarMovies(Movie movie)
    {
        return repository.CreateQuery()
            .Include(m => m.Genres)
            .Where(m => m.Genres.Any(g => movie.Genres.Contains(g)))
            .Where(m =>m != movie)
            .Take(4)
            .ToList();
    }
}
