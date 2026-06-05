using CineScope.Models;
using CineScope.Services;
using CineScope.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineScope.Controllers;

public class MovieController(MovieRepository repository, GenreRepository genreRepository) : Controller
{
    [HttpGet("/movies")]
    public IActionResult Index(FilterModel filter)
    {
        IQueryable<Movie> query = repository.CreateQuery()
            .Include(m => m.Genres);
        
        query = filter.ApplySearch(query);
        query = filter.ApplySortBy(query);
        query = filter.ApplyGenres(query);
        query = filter.ApplyRating(query);

        var movies = query.ToList();

        var genres = genreRepository.CreateQuery()
            .OrderByDescending(g => g.Movies.Count)
            .ToList();
        
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
        
        var similarMoviesFilter = CreateFilter(movie);

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
        ViewBag.SimilarMovies = GetSimilarMovies(movie, similarMoviesFilter);
        ViewBag.SimilarMoviesFilter = similarMoviesFilter;

        return View();
    }

    private List<Movie> GetSimilarMovies(Movie movie, FilterModel filter)
    {
        IQueryable<Movie> query = repository.CreateQuery()
            .Include(m => m.Genres);

        query = filter.ApplyGenres(query);
        
        return query
            .Where(m => m != movie)
            .Take(4)
            .ToList();
    }

    private FilterModel CreateFilter(Movie movie)
    {
        return new FilterModel
        {
            Genres = movie.Genres.Select(g => g.Id).ToList()
        };
    }
}