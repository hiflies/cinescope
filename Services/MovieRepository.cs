using CineScope.Models;
using Microsoft.EntityFrameworkCore;

namespace CineScope.Services;

public class MovieRepository(CineScopeDbContext context)
{
    public IQueryable<Movie> GetAll()
    {
        return context.Movies;
    }

    public Task<Movie?> Get(int id)
    {
        return context.Movies.Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id == id);
    }

    public void Save()
    {
        context.SaveChanges();
    }

    public void Add(Movie movie)
    {
        context.Movies.Add(movie);
    }

    public Task DeleteAsync(Movie movie)
    {
        context.Movies.Remove(movie);
        return context.SaveChangesAsync();
    }
}