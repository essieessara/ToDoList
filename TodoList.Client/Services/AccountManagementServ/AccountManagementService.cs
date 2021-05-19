
using System;
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
                await _localstorage.CallLocalStorage<SuccesLogin>("userToken");
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
