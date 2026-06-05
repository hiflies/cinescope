using CineScope.Entities;
using CineScope.Models;
using CineScope.Services;
using CineScope.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineScope.Controllers;

[Authorize(Roles = "admin")]
public class AdminMovieController(MovieRepository repository, GenreRepository genreRepository, TmdbApiService tmdbApi)
    : Controller
{
    [HttpGet("/admin/movies")]
    public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] string search = "")
    {
        const int size = 10;
        if (page < 1) page = 1;
        var skip = (page - 1) * size;

        var query = repository.CreateQuery();
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(m => m.Title.Contains(search, StringComparison.CurrentCultureIgnoreCase));
        }

        var movies = await query
            .OrderBy(m => m.Id)
            .Include(m => m.Genres)
            .Skip(skip)
            .Take(size)
            .ToListAsync();

        int movieCount;
        if (!string.IsNullOrEmpty(search))
        {
            movieCount = await repository.CreateQuery().CountAsync(m => m.Title.Contains(search, StringComparison.CurrentCultureIgnoreCase));
        }
        else
        {
            movieCount = await repository.CreateQuery().CountAsync();
        }

        ViewBag.Movies = movies;
        ViewBag.MovieCount = movieCount;
        ViewBag.Page = page;
        ViewBag.Size = size;
        ViewBag.Search = search;

        return View();
    }

    [HttpPost("/admin/movies/delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await repository.Get(id);
        if (movie != null)
        {
            await repository.DeleteAsync(movie);
        }

        return Redirect("/admin/movies");
    }

    [HttpGet("/admin/movies/add")]
    public async Task<IActionResult> Add()
    {
        ViewBag.Genres = await genreRepository.CreateQuery().ToListAsync();
        return View();
    }

    [HttpPost("/admin/movies/add")]
    public async Task<IActionResult> AddPost(
        string title, string tagline, string releaseYear, string duration,
        string ageRating, string description, string director, string actors,
        string posterUrl, string backdropImageUrl, List<int> selectedGenreIds)
    {
        var genres = await genreRepository.CreateQuery().ToListAsync();
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

        repository.Add(movie);
        repository.Save();
        return Redirect("/admin/movies");
    }

    [HttpGet("/admin/movies/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var movie = await repository.Get(id);
        if (movie == null) return NotFound();

        ViewBag.MovieId = movie.Id;
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
        ViewBag.Genres = await genreRepository.CreateQuery().ToListAsync();

        return View();
    }

    [HttpPost("/admin/movies/{id:int}")]
    public async Task<IActionResult> EditPost(int id,
        string title, string tagline, string releaseYear, string duration,
        string ageRating, string description, string director, string actors,
        string posterUrl, string backdropImageUrl, List<int> selectedGenreIds)
    {
        var movie = await repository.Get(id);
        if (movie == null) return NotFound();

        var genres = await genreRepository.CreateQuery().ToListAsync();

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

        repository.Save();
        return Redirect("/admin/movies");
    }

    [HttpGet("/admin/movies/{id:int}/update")]
    public async Task<IActionResult> UpdateFromTmdb(int id)
    {
        var movie = await repository.Get(id);
        if (movie == null) return NotFound();

        var tmdbMovie = await tmdbApi.GetMovieById((int)movie.TmdbId!);
        if (tmdbMovie == null) return NotFound();

        var allGenres = await genreRepository.CreateQuery().ToListAsync();

        TmdbModelTransformer.Load(movie, tmdbMovie);
        List<Genre> newGenres;
        movie.Genres =
            TmdbModelTransformer.LoadGenres(tmdbMovie.Genres.Select(g => g.Name).ToList(), allGenres, out newGenres);
        genreRepository.Add(newGenres);

        genreRepository.Save();
        repository.Save();
        return Redirect($"/admin/movies/{id}");
    }
}