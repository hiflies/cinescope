using System.Text.Json.Serialization;

namespace CineScope.Models;

public class TmdbMovieSearchModel
{
    [JsonPropertyName("results")]
    public List<TmdbMovieModel> Results { get; set; }
}
