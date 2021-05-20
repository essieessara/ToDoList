
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using TodoList.Client.Helpers;
using TodoList.Client.Services.ApiClient;

namespace TodoList.Client.Services.AccountManagementServ
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IApiClient _httpClient;
        private readonly ILocalStorage _localstorage;
        string responseBody = string.Empty;
        public AccountManagementService(IApiClient httpClient, ILocalStorage localstorage)
        {
            _httpClient = httpClient;
            _localstorage = localstorage;
        }

        public async Task<SuccesLogin> LoginAsync(LoginUserModel model)
        {
            try
            {
                SuccesLogin result = await _httpClient.PostAsync<SuccesLogin>("Account/Login", model);

                await _localstorage.AddLocalStorageAsync("userToken", result);


                var handler = new JwtSecurityTokenHandler();

                var decodedValue = handler.ReadJwtToken(result.TokenString);

                var sid = decodedValue.Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).FirstOrDefault();
                var username = decodedValue.Claims.Where(c => c.Type == "unique_name")
                   .Select(c => c.Value).FirstOrDefault();
                var fullname = decodedValue.Claims.Where(c => c.Type == "given_name")
                   .Select(c => c.Value).FirstOrDefault();
                return result;
            }
            catch (Exception e)
            {
                responseBody = e.Message;
                throw new Exception(responseBody);
            }

        }
    }
}
