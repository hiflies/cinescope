using CineScope.Entities;
using CineScope.Models;

namespace CineScope.Utils;

public class TmdbModelTransformer
{
    public static void Load(Movie movie, TmdbMovieModel tmdbMovie)
    {
        movie.Title = tmdbMovie.Title;
        movie.Tagline = tmdbMovie.Tagline;
        movie.ReleaseYear = DateTime.Parse(tmdbMovie.ReleaseDate).Year;
        movie.Duration = tmdbMovie.Runtime;
        movie.Description = tmdbMovie.Overview;
        movie.PosterUrl = $"https://www.themoviedb.org/t/p/w1280/{tmdbMovie.PosterPath}";
        movie.BackdropImageUrl = $"https://www.themoviedb.org/t/p/w1280/{tmdbMovie.BackdropPath}";
        movie.Rating = tmdbMovie.VoteAverage;
        movie.Actors = string.Join(", ", tmdbMovie.Credits.Cast.Select(c => c.Name));
        movie.Director = tmdbMovie.Credits.Crew.First(c => c.Job == "Director").Name;
        movie.AgeRating = tmdbMovie.ReleaseDates.Results.FirstOrDefault(r => r.Country == "SE")?.ReleaseDates
            ?.FirstOrDefault(n=>n.Certification != "")?.Certification ?? "NR";
    }

    public static List<Genre> LoadGenres(List<string> genres, List<Genre> existingGenres, out List<Genre> newGenres)
    {
        var output = new List<Genre>();
        newGenres = new List<Genre>();

        foreach (var genreName in genres)
        {
            var genre = existingGenres.FirstOrDefault(g =>
                string.Equals(g.Name, genreName, StringComparison.CurrentCultureIgnoreCase)
            );
            if (genre == null)
            {
                genre = new Genre
                {
                    Name = genreName
                };
                newGenres.Add(genre);
            }

            output.Add(genre);
        }

        return output;
    }
}