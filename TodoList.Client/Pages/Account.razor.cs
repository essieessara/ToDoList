using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Client.Pages
{
    public partial class Account
    {
        [Inject]
        public IJSRuntime JS { get; set; }
        public async Task NavigateToResetPassword()
        {
            string url = "/ResetPassword";
            await JS.InvokeAsync<object>("open", url, "_parent");
        }

        public async Task NavigateToLogout()
        {
            string url = "/Logout";
            await JS.InvokeAsync<object>("open", url, "_parent");
        }
        public async Task NavigateToLogin()
        {
            string url = "/Login";
            await JS.InvokeAsync<object>("open", url, "_parent");
        }

        public async Task NavigateToRegister()
        {
            string url = "/Signup";
            await JS.InvokeAsync<object>("open", url, "_parent");
        }
    }
}
