using System.Net.Http.Headers;
using CineScope.Models;

namespace CineScope.Services;

public class TmdbApiService(string apiKey)
{
    public async Task<TmdbMovieModel?> GetMovieById(int movieId)
    {
        var client = new HttpClient();

        var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.themoviedb.org/3/movie/{movieId}?append_to_response=credits,release_dates");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var response = await client.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<TmdbMovieModel>();
    }

    public async Task<TmdbMovieSearchModel> SearchMovies(string search)
    {
        var client = new HttpClient();

        var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.themoviedb.org/3/search/movie?query={search}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var response = await client.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<TmdbMovieSearchModel>();
    }
}
