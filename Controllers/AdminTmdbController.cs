using CineScope.Entities;
using CineScope.Services;
using CineScope.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineScope.Controllers;

[Authorize(Roles = "admin")]
public class AdminTmdbController(MovieRepository repository, GenreRepository genreRepository, TmdbApiService tmdbApi)
    : Controller
{
    [HttpGet("/admin/tmdb")]
    public async Task<IActionResult> Index([FromQuery] string search = "")
    {
        var tmdbMovies = await tmdbApi.SearchMovies(search);
        var tmdbIdList = tmdbMovies.Results.Select(movie => movie.Id).ToList();
        var existingMovies = repository.CreateQuery()
            .Where(m => m.TmdbId != null && tmdbIdList.Contains((int)m.TmdbId))
            .ToDictionary(m => (int)m.TmdbId!);

        ViewBag.ExistingMovies = existingMovies;
        ViewBag.Movies = tmdbMovies.Results;
        ViewBag.Search = search;

        return View();
    }

    [HttpGet("/admin/tmdb/{id:int}")]
    public async Task<IActionResult> FetchFromTmdb(int id)
    {
        var tmdbMovie = await tmdbApi.GetMovieById(id);
        if (tmdbMovie == null) return NotFound();

        var movie = await repository.GetByTmdbId(id);
        if (movie == null)
        {
            movie = new Movie
            {
                TmdbId = id
            };
            repository.Add(movie);
        }

        var allGenres = await genreRepository.CreateQuery().ToListAsync();

        TmdbModelTransformer.Load(movie, tmdbMovie);
        movie.Genres = TmdbModelTransformer.LoadGenres(
            tmdbMovie.Genres.Select(g => g.Name).ToList(),
            allGenres,
            out var newGenres
        );
        genreRepository.Add(newGenres);

        genreRepository.Save();
        repository.Save();
        return Redirect($"/admin/movies");
    }
}