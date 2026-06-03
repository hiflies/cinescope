using CineScope.Models;
using CineScope.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineScope.Controllers;

public class AdminMovieController : Controller
{
    private readonly MovieRepository _repository;
    private readonly GenreRepository _genreRepository;

    public AdminMovieController(MovieRepository repository, GenreRepository genreRepository)
    {
        _repository = repository;
        _genreRepository = genreRepository;
    }

    [HttpGet("/admin/movies")]
    public async Task<IActionResult> Index([FromQuery] int page = 1)
    {
        const int size = 3;
        if (page < 1) page = 1;
        var skip = (page - 1) * size;

        var movies = await _repository.GetAll()
            .OrderBy(m => m.Id)
            .Include(m => m.Genres)
            .Skip(skip)
            .Take(size)
            .ToListAsync();

        var movieCount = await _repository.GetAll().CountAsync();

        ViewBag.Movies = movies;
        ViewBag.MovieCount = movieCount;
        ViewBag.Page = page;
        ViewBag.Size = size;

        return View();
    }

    [HttpPost("/admin/movies/delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _repository.Get(id);
        if (movie != null)
        {
            await _repository.DeleteAsync(movie);
        }
        return Redirect("/admin/movies");
    }

    [HttpGet("/admin/movies/add")]
    public async Task<IActionResult> Add()
    {
        ViewBag.Genres = await _genreRepository.GetAll().ToListAsync();
        return View();
    }

    [HttpPost("/admin/movies/add")]
    public async Task<IActionResult> AddPost(
        string title, string tagline, string releaseYear, string duration,
        string ageRating, string description, string director, string actors,
        string posterUrl, string backdropImageUrl, List<int> selectedGenreIds)
    {
        var genres = await _genreRepository.GetAll().ToListAsync();
        ViewBag.Genres = genres;

        var movie = new Movie
        {
            Title = title,
            Tagline = tagline,
            ReleaseYear = int.Parse(releaseYear),
            Duration = int.Parse(duration),
            AgeRating = ageRating,
            Description = description,
            Director = director,
            Actors = actors,
            PosterUrl = posterUrl,
            BackdropImageUrl = backdropImageUrl,
            Genres = genres.Where(g => selectedGenreIds.Contains(g.Id)).ToList()
        };

        _repository.Add(movie);
        _repository.Save();
        return Redirect("/admin/movies");
    }

    [HttpGet("/admin/movies/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var movie = await _repository.Get(id);
        if (movie == null) return NotFound();

        ViewBag.MovieTitle = movie.Title;
        ViewBag.Tagline = movie.Tagline;
        ViewBag.ReleaseYear = movie.ReleaseYear.ToString();
        ViewBag.Duration = movie.Duration.ToString();
        ViewBag.AgeRating = movie.AgeRating;
        ViewBag.Description = movie.Description;
        ViewBag.Director = movie.Director;
        ViewBag.Actors = movie.Actors;
        ViewBag.PosterUrl = movie.PosterUrl;
        ViewBag.BackdropImageUrl = movie.BackdropImageUrl;
        ViewBag.SelectedGenreIds = movie.Genres.Select(g => g.Id).ToList();
        ViewBag.Genres = await _genreRepository.GetAll().ToListAsync();

        return View();
    }

    [HttpPost("/admin/movies/{id:int}")]
    public async Task<IActionResult> EditPost(int id,
        string title, string tagline, string releaseYear, string duration,
        string ageRating, string description, string director, string actors,
        string posterUrl, string backdropImageUrl, List<int> selectedGenreIds)
    {
        var movie = await _repository.Get(id);
        if (movie == null) return NotFound();

        var genres = await _genreRepository.GetAll().ToListAsync();

        movie.Title = title;
        movie.Tagline = tagline;
        movie.ReleaseYear = int.Parse(releaseYear);
        movie.Duration = int.Parse(duration);
        movie.AgeRating = ageRating;
        movie.Description = description;
        movie.Director = director;
        movie.Actors = actors;
        movie.PosterUrl = posterUrl;
        movie.BackdropImageUrl = backdropImageUrl;
        movie.Genres = genres.Where(g => selectedGenreIds.Contains(g.Id)).ToList();

        _repository.Save();
        return Redirect("/admin/movies");
    }
}
