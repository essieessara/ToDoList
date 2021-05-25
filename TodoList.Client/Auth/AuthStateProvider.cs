using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using TodoList.Client.Helpers;

namespace TodoList.Client.Auth
{
    public class AuthStateProvider : AuthenticationStateProvider
    {

        private readonly ILocalStorage _storage;
        private readonly AuthenticationState _anonymous;

        public AuthStateProvider(ILocalStorage storage)
        {
            this._storage = storage;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(1500);
            var token = await _storage.CallLocalStorageAsync<SuccesLogin>("userToken");

            if (token is null)
            {
                return _anonymous;
            }
            if (string.IsNullOrWhiteSpace(token.TokenString))
            {
                return await Task.FromResult(_anonymous);
            }
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var decodedValue = handler.ReadJwtToken(token.TokenString);
                if ((decodedValue.ValidTo > DateTime.UtcNow) && (decodedValue.ValidFrom < DateTime.UtcNow))
                {
                    return
                     new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(decodedValue.Claims, "AuthType")));
                }
                else
                    return await Task.FromResult(_anonymous);

            }

        }
    }
}
