using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using TodoList.Client.Services.AccountManagementServ;

namespace TodoList.Client.Pages
{
    public partial class Signup
    {
        public RegisterUserModel registerUser = new();
        string responseBody = string.Empty;


        [Inject]
        public IAccountManagementService _account { get; set; }

        private async Task RegisterUser()
        {


            try
            {
                await _account.SignupAsync(registerUser);
                responseBody = "Please press the Login button to continue";
            }
            catch (Exception e)
            {
                responseBody = e.Message;
            }


        }

    }


}



