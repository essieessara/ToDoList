using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TodoList.Client.Helpers;
using TodoList.Client.Services.AccountManagementServ;
using TodoList.Client.Services.ApiClient;

namespace TodoList.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("backEnd", confing =>
            {
                confing.BaseAddress = new Uri("https://localhost:44325/api/");
                confing.DefaultRequestHeaders.Clear();
                confing.DefaultRequestHeaders.Add("Accept", "*/*");
                confing.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped<IAccountManagementService, AccountManagementService>();
            builder.Services.AddScoped<IApiClient, ApiClient>();
            builder.Services.AddScoped<ILocalStorage, LocalStorage>();
            await builder.Build().RunAsync();
        }
    }
}
