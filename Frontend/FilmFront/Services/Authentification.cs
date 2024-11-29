using Microsoft.AspNetCore.Components.WebAssembly.Storage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

using System.Security.Claims;
using Frontend.SharedModels;

namespace FilmFront.Services
{
    public class Authentification : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _sessionStorage;

        public Authentification(ProtectedLocalStorage protectedSessionStorage)
        {
            _sessionStorage = protectedSessionStorage;            
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = await _sessionStorage.GetAsync<User>("User");
            if (user.Value != null)
            {
                var claim = GenerateClaimsPrincipal(user.Value);
                return new AuthenticationState(claim);
            }
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        // Corrected Login method
        public async Task Login(User user, string token)
        {
            await _sessionStorage.SetAsync("User", user);
            await _sessionStorage.SetAsync("Token", token);
            ClaimsPrincipal claim = GenerateClaimsPrincipal(user);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claim)));
        }

        public ClaimsPrincipal GenerateClaimsPrincipal(User user)
        {
            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Prenom),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "custom");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            return claimsPrincipal;
        }

        // Corrected Logout method
        public async Task Logout()
        {
            await _sessionStorage.DeleteAsync("User");
            await _sessionStorage.DeleteAsync("Token");
            var claimDisconnected = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimDisconnected)));        
        }
    }
}
