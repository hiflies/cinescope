using CineScope.Entities;

namespace CineScope.Services;

public class UserRepository(CineScopeDbContext context)
{
    public void Add(User user)
    {
        context.Users.Add(user);
    }

    public void Save()
    {
        context.SaveChanges();
    }

    public User? GetByIdentifier(string identifier)
    {
        return context.Users.FirstOrDefault(u => u.Email == identifier || u.Username == identifier);
    }
}