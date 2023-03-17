using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WebAssembly.Shared.Models;

namespace WebAssembly.Client.Helpers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService sessionStorageService;
        private ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            this.sessionStorageService = sessionStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await sessionStorageService.ReadEncryptedItemASync<UserSession>("UserSession");
                if (userSession == null) return await Task.FromResult(new AuthenticationState(anonymous));
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userSession.mail),
                    new Claim(ClaimTypes.Name, $"{userSession.name} {userSession.surname}"),
                    new Claim(ClaimTypes.Sid, userSession.id.ToString()),
                    new Claim(ClaimTypes.Role, userSession.role)
                }, "JwtAuth"));

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));

            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public async Task UpdateAuthenticationState(UserSession? userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userSession.mail),
                    new Claim(ClaimTypes.Name, $"{userSession.name} {userSession.surname}"),
                    new Claim(ClaimTypes.Sid, userSession.id.ToString()),
                    new Claim(ClaimTypes.Role, userSession.role)
                }));

                userSession.ExpiryTimeStamp = DateTime.Now.AddSeconds(userSession.ExpiresIn);
                await sessionStorageService.SaveItemEncryptedAsync("UserSession", userSession);
            }
            else 
            {
                claimsPrincipal = anonymous;
                await sessionStorageService.RemoveItemAsync("UserSession");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task<string> GetToken() 
        {
            var result = string.Empty;

            try
            {
                var userSession = await sessionStorageService.ReadEncryptedItemASync<UserSession>("UserSession");
                if (userSession != null && DateTime.Now < userSession.ExpiryTimeStamp) {
                    result = userSession.Token;
                }
            }
            catch
            {
                
            }

            return result;
        }
    }
}
