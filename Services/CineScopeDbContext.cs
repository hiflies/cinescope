using CineScope.Entities;
using Microsoft.EntityFrameworkCore;

namespace CineScope.Services;

public class CineScopeDbContext(DbContextOptions<CineScopeDbContext> options) : DbContext(options)
{
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<User> Users => Set<User>();
}