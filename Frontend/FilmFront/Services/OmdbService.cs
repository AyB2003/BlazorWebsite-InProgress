using Frontend.SharedModels;


namespace FilmFront.Services
{
    public class OmdbService
    {
        private readonly HttpClient _httpClient;
        public OmdbService(HttpClient httpClient){
            _httpClient = httpClient;
        }
        public async Task<List<Film>> GetFilms(){
            var films = await _httpClient.GetFromJsonAsync<List<Film>>("http://localhost:5042/api/Film/Films");
            return films;
        }
    }
}