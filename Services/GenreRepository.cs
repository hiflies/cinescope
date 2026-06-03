using CineScope.Models;

namespace CineScope.Services;

public class GenreRepository(CineScopeDbContext context)
{
    public IQueryable<Genre> GetAll(){
        return context.Genres;
    }

    public ValueTask<Genre?> Get(int id)
    {
        return context.Genres.FindAsync(id);
    }
}