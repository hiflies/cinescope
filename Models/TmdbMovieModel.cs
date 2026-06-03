using System.Text.Json.Serialization;

namespace CineScope.Models;

public class TmdbMovieModel
{
    [JsonPropertyName("backdrop_path")]
    public string BackdropPath { get; set; }

    [JsonPropertyName("genres")]
    public List<Genre> Genres { get; set; }
    
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("overview")]
    public string Overview { get; set; }
    
    [JsonPropertyName("poster_path")]
    public string PosterPath { get; set; }
    
    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; }
    
    [JsonPropertyName("runtime")]
    public int Runtime { get; set; }
    
    [JsonPropertyName("tagline")]
    public string Tagline { get; set; }
    
    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; }
    
    [JsonPropertyName("vote_count")]
    public int VoteCount { get; set; }
    
    [JsonPropertyName("credits")]
    public CreditsModel Credits { get; set; }
    
    [JsonPropertyName("release_dates")]
    public ReleaseDatesModel ReleaseDates { get; set; }
    
    public class Genre
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
    
    public class CreditsModel
    {
        [JsonPropertyName("cast")]
        public List<CastModel> Cast { get; set; }
        
        [JsonPropertyName("crew")]
        public List<CrewModel> Crew { get; set; }
        
        public class CastModel
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
        
            [JsonPropertyName("order")]
            public int Order { get; set; }
        }
    
        public class CrewModel
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
        
            [JsonPropertyName("job")]
            public string Job { get; set; }
        }
    }
    
    public class ReleaseDatesModel
    {
        [JsonPropertyName("results")]
        public List<ResultModel> Results { get; set; }

        public class ResultModel
        {
            [JsonPropertyName("iso_3166_1")]
            public string Country { get; set; }
            
            [JsonPropertyName("release_dates")]
            public List<ReleaseDateModel> ReleaseDates { get; set; }

            public class ReleaseDateModel
            {
                [JsonPropertyName("certification")]
                public string Certification { get; set; }
            }
        }
    }
}
