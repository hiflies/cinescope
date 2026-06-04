using System.Dynamic;
using CineScope.Models;
using CineScope.Services;
using CineScope.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineScope.Controllers;

public class MovieController(MovieRepository repository, GenreRepository genreRepository) : Controller
{
    [HttpGet("/movies")]
    public IActionResult Index(
        [FromQuery] string search = "",
        [FromQuery] string sortBy = "newest",
        [FromQuery] double rating = 0,
        [FromQuery(Name = "genreId[]")] List<int>? genreIdList = null
    )
    {
        IQueryable<Movie> query = repository.CreateQuery()
            .Include(m => m.Genres);

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(m => m.Title.ToLower().Contains(search.ToLower()));
        }

        if (!string.IsNullOrEmpty(sortBy))
        {
            if (sortBy == "rating")
            {
                query = query.OrderByDescending(m => m.Rating);
            }
            else if (sortBy == "newest")
            {
                query = query.OrderByDescending(m => m.ReleaseYear).ThenByDescending(m => m.Rating);
            }
            else if (sortBy == "recent")
            {
                query = query.OrderByDescending(m => m.Id);
            }
        }

        if (genreIdList != null && genreIdList.Count > 0)
        {
            query = query.Where(m => m.Genres.Any(g => genreIdList.Contains(g.Id)));
        }

        if (rating != 0)
        {
            query = query.Where(m => m.Rating >= rating);
        }

        var movies = query.ToList();

        var genres = genreRepository.CreateQuery()
            .OrderByDescending(g => g.Movies.Count)
            .ToList();

        dynamic filter = new ExpandoObject();
        filter.Genres = genreIdList ?? [];
        filter.Search = search;
        filter.Rating = rating;
        filter.SortBy = sortBy;
        
        ViewBag.Filter = filter;
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
            .Where(m => m != movie)
            .Take(4)
            .ToList();
    }
}