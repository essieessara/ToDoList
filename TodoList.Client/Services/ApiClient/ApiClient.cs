using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;

namespace TodoList.Client.Services.ApiClient
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        public ApiClient(IHttpClientFactory httpClient)
           => _httpClient = httpClient.CreateClient("backEnd");
      


        public async Task<T> PostAsync <T> (string URL, object Data)
        {
            string response = string.Empty;
            try
            {

                string serializedUser = JsonConvert.SerializeObject(Data);
                StringContent content = new StringContent(serializedUser, 
                    System.Text.Encoding.UTF8,"application/json");
                var result = await _httpClient.PostAsync(URL, content);
                response =  result.Content.ReadAsStringAsync().Result;
                result.EnsureSuccessStatusCode();
                T outPut = JsonConvert.DeserializeObject<T>(response);
                return outPut;

            }
            catch (Exception e)
            {
                throw new Exception(response);
            }
        }

    }
}
