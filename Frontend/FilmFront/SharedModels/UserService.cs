using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FilmFront.Components.SharedModels;
namespace FilmFront.Components.SharedModels
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Userinfo> Login(Userinfo userinfo)
        {

            var response = await _httpClient.PostAsJsonAsync($"api/User/login", userinfo);
            if (response == null)
            {
                return null;
            }
            else{
                return await response.Content.ReadFromJsonAsync<Userinfo>();
            }
        }
    }
}