using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace DentalScheduler.Web.UI
{
    public class ApplicationAuthenticationStateProvider : AuthenticationStateProvider
    {
        public ApplicationAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
        }

        public ILocalStorageService LocalStorage { get; }

        public string AccessToken { get; private set; }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claimsPrincipal = await GetClaimsPrincipalAsync();
            return new AuthenticationState(claimsPrincipal);
        }

        public async Task MarkUserAsLoggedInAsync(
            IUserCredentialsInput credentials, 
            string accessToken)
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            var roleName = jwtSecurityToken.Claims
                .FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))
                ?.Value;
            System.Console.WriteLine($" Role Name: {roleName}");

            await LocalStorage.SetItemAsync("UserName", credentials.UserName);
            await LocalStorage.SetItemAsync("Role", roleName);
            await LocalStorage.SetItemAsync("AccessToken", accessToken);

            var claimsPrincipal = await GetClaimsPrincipalAsync();

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOutAsync()
        {
            await LocalStorage.RemoveItemAsync("UserName");
            await LocalStorage.RemoveItemAsync("Role");
            await LocalStorage.RemoveItemAsync("AccessToken");

            var claimsPrincipal = await GetClaimsPrincipalAsync();
            
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        private async Task<ClaimsPrincipal> GetClaimsPrincipalAsync()
        {
            var claims = new List<Claim>();
            
            if (await LocalStorage.ContainKeyAsync("UserName"))
            {
                var userName = await LocalStorage.GetItemAsync<string>("UserName");
                claims.Add(new Claim(ClaimTypes.Name, userName));
            }

            if (await LocalStorage.ContainKeyAsync("Role"))
            {
                var roleName = await LocalStorage.GetItemAsync<string>("Role");
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            var identity =  claims.Count > 0 
                ? new ClaimsIdentity(claims, "apiauth_type")
                : new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return claimsPrincipal;
        }
    }
}