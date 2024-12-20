using Microsoft.AspNetCore.Http.HttpResults;
using TrackerDeFavorisApi.Models;
namespace Backend.Services
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