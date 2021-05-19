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

        //public Login(IAccountManagementService account)
        //    => _account = account;

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
            //finally
            //{
            //    this.StateHasChanged();
            //}

            //try
            //{
            //    string serializedUser = JsonConvert.SerializeObject(loginModel);


            //    HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            //    httpRequestMessage.Method = new HttpMethod("POST");
            //    httpRequestMessage.RequestUri = new Uri("./Account/Login");
            //    httpRequestMessage.Content = new StringContent(serializedUser);
            //    httpRequestMessage.Content.Headers.ContentType
            //        = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            //    var response = await Http.SendAsync(httpRequestMessage);
            //    response.EnsureSuccessStatusCode();
            //    responseBody = await response.Content.ReadAsStringAsync();

            //}
            //catch (Exception)
            //{

            //    responseBody = "something went wrong !";
            //}
            //finally
            //{
            //    this.StateHasChanged();
            //}



        }


    }
}
