using CineScope.Entities;
using CineScope.Models;

namespace CineScope.Services;

public class GenreRepository(CineScopeDbContext context)
{
    public IQueryable<Genre> CreateQuery(){
        return context.Genres;
    }

    public ValueTask<Genre?> Get(int id)
    {
        return context.Genres.FindAsync(id);
    }

    public void Add(List<Genre> newGenres)
    {
        context.Genres.AddRange(newGenres);
    }

    public void Save()
    {
        context.SaveChanges();
    }
}