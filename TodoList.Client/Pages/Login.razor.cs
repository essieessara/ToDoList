using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using TodoList.Client.Services.AccountManagementServ;

namespace TodoList.Client.Pages
{
    public partial class Login
    {
        public LoginUserModel loginModel = new();
        string responseBody = string.Empty;
        [Inject]
        public IAccountManagementService _account { get; set; }
        [Inject]
        public ILocalStorageService _storage { get; set; }

        private async Task ValidateUser()
        {


            try
            {
                SuccesLogin tokenString = await _account.LoginAsync(loginModel);
                await _storage.SetItemAsync("userToken", tokenString);
                await _storage.GetItemAsync<SuccesLogin>("userToken");
            }
            catch (Exception e)
            {

                responseBody = e.Message;
            }

        }


    }
}
