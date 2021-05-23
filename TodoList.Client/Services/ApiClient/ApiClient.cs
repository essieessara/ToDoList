using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using TodoList.Client.Helpers;

namespace TodoList.Client.Services.ApiClient
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorage _localstorage;
        private bool isAuthenticated;

        public ApiClient(IHttpClientFactory httpClient , ILocalStorage localstorage)
        { 
            _httpClient = httpClient.CreateClient("backEnd");
            _localstorage = localstorage;
        }




        public async Task<T> PostAsync<T>(string URL, object Data)
        {
            string response = string.Empty;
            try
            {
                isAuthenticated = false;
                string serializedUser = JsonConvert.SerializeObject(Data);
                StringContent content = new StringContent(serializedUser,
                    System.Text.Encoding.UTF8, "application/json");
                var result = await _httpClient.PostAsync(URL, content);
                response = result.Content.ReadAsStringAsync().Result;
                result.EnsureSuccessStatusCode();
                T outPut = JsonConvert.DeserializeObject<T>(response);
                return outPut;

            }
            catch (Exception e)
            {
                throw new Exception(response);
            }
        }

        public async Task<T> PutAsync<T>(string URL, object Data)
        {
            string response = string.Empty;
            try
            {
                isAuthenticated = true;
                string serializedUser = JsonConvert.SerializeObject(Data);
                StringContent content = new StringContent(serializedUser,
                    System.Text.Encoding.UTF8, "application/json");
                await authenticatedAsync();
                var result = await _httpClient.PutAsync(URL,content);
                response = result.Content.ReadAsStringAsync().Result;
                result.EnsureSuccessStatusCode();
                T outPut = JsonConvert.DeserializeObject<T>(response);
                return outPut;

            }
            catch (Exception e)
            {
                throw new Exception(response);
            }
        }
        public async Task<HttpResponseMessage> GetAsync(string URL)
        {

            try
            {
                isAuthenticated = false;
                var request = await _httpClient.GetAsync(URL);

                return request;

            }
            catch (Exception e)
            {
                throw new Exception("something went wrong");
            }

           
        }

        private async Task<AuthenticationHeaderValue> authenticatedAsync()
        {
            if (isAuthenticated == true)
            {
                var token = await _localstorage.CallLocalStorageAsync<SuccesLogin>("userToken");
                var auth = _httpClient.DefaultRequestHeaders.Authorization =
                     new AuthenticationHeaderValue("Bearer", token.TokenString);
                return auth;
            }
            else
                return null;
        }
    }
}
