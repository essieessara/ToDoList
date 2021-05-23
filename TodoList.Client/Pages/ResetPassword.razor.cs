using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using TodoList.Client.Services.AccountManagementServ;

namespace TodoList.Client.Pages
{
    public partial class ResetPassword
    {
        public ResetPasswordModel ResetUserPassword = new();
        string responseBody = string.Empty;


        [Inject]
        public IAccountManagementService _account { get; set; }

        private async Task Resetpass()
        {


            try
            {
                await _account.ResetPasswordAsync(ResetUserPassword);
                responseBody = "Password is changed successfuly";
            }
            catch (Exception e)
            {
                responseBody = e.Message;
            }


        }
    }
}
