using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FilmFront.Components.SharedModels;

namespace FilmFront.Components.SharedModels
{
    public class FilmService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient){
            _httpClient = httpClient;
        }
        public async

    }
}