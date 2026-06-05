using System.Text;
using CineScope.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CineScope.Models;

public class FilterModel
{
    [FromQuery] public string Search { get; set; } = string.Empty;
    [FromQuery] public string SortBy { get; set; } = string.Empty;
    [FromQuery] public double Rating { get; set; } = 0;
    [FromQuery(Name = "genreId[]")] public List<int> Genres { get; set; } = [];

    public IQueryable<Movie> ApplySearch(IQueryable<Movie> query)
    {
        if (string.IsNullOrEmpty(Search))
        {
            return query;
        }

        return query.Where(m => m.Title.ToLower().Contains(Search.ToLower()));
    }

    public IQueryable<Movie> ApplySortBy(IQueryable<Movie> query)
    {
        return SortBy switch
        {
            "rating" => query.OrderByDescending(m => m.Rating),
            "newest" => query.OrderByDescending(m => m.ReleaseYear).ThenByDescending(m => m.Rating),
            "recent" => query.OrderByDescending(m => m.Id),
            _ => query
        };
    }

    public IQueryable<Movie> ApplyGenres(IQueryable<Movie> query)
    {
        if (Genres == null || Genres.Count == 0)
        {
            return query;
        }

        return query.Where(m => m.Genres.Any(g => Genres.Contains(g.Id)));
    }

    public IQueryable<Movie> ApplyRating(IQueryable<Movie> query)
    {
        if (Rating == 0)
        {
            return query;
        }

        return query.Where(m => m.Rating >= Rating);
    }

    public FilterModel WithoutSearch()
    {
        return new FilterModel
        {
            SortBy = SortBy,
            Rating = Rating,
            Genres = Genres,
        };
    }

    public FilterModel WithoutSortBy()
    {
        return new FilterModel
        {
            Search = Search,
            Rating = Rating,
            Genres = Genres,
        };
    }

    public FilterModel WithoutRating()
    {
        return new FilterModel
        {
            Search = Search,
            SortBy = SortBy,
            Genres = Genres,
        };
    }

    public FilterModel WithoutGenre(int genreId)
    {
        return new FilterModel
        {
            Search = Search,
            SortBy = SortBy,
            Rating = Rating,
            Genres = Genres.Where(g => g != genreId).ToList(),
        };
    }

    public override string ToString()
    {
        var output = new List<string>();

        if (!string.IsNullOrEmpty(Search))
        {
            output.Add($"search={Search}");
        }

        if (!string.IsNullOrEmpty(SortBy))
        {
            output.Add($"sortBy={SortBy}");
        }

        if (Genres != null && Genres.Count > 0)
        {
            output.AddRange(Genres.Select(genre => $"genreId[]={genre}"));
        }

        if (Rating != 0)
        {
            output.Add($"rating={Rating}");
        }

        if (output.Count == 0)
        {
            return string.Empty;
        }

        return "?" + string.Join("&", output);
    }
}