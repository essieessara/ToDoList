
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using TodoList.Client.Services.ApiClient;

namespace TodoList.Client.Services.AccountManagementServ
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IApiClient _httpClient;
        public AccountManagementService(IApiClient httpClient)
           => _httpClient = httpClient;

        public async Task<SuccesLogin> LoginAsync(LoginUserModel model)
        {
            SuccesLogin result = await _httpClient.PostAsync<SuccesLogin>("Account/Login", model);
            return result;
        }
    }
}
