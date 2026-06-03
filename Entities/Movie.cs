namespace CineScope.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<Genre> Genres { get; set; }
    public int ReleaseYear { get; set; }
    public double Rating { get; set; }
    public int Duration { get; set; }
    public string PosterUrl { get; set; }
    public string BackdropImageUrl { get; set; }
    public string Description { get; set; }
    public string Tagline { get; set; }
    public string Director { get; set; }
    public string Actors { get; set; }
    public string AgeRating { get; set; }
    public int? TmdbId { get; set; }
}