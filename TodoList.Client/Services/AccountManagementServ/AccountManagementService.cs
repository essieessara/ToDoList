using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using TodoList.Client.Services.HttpClientServ;

namespace TodoList.Client.Services.AccountManagementServ
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IHttpClientService _httpClient;
        public AccountManagementService(IHttpClientService httpClient)
           => _httpClient = httpClient;

        public async Task<T> LoginAsync<T>(string URL, object Data)
        {
            var result = await _httpClient.PostAsync<T>(URL, Data);
            return result;
        }
    }
}
