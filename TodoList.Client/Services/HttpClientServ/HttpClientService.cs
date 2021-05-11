using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TodoList.Client.Services.HttpClientServ
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(IHttpClientFactory httpClient)
           => _httpClient = httpClient.CreateClient();
        

        public async Task<T> PostAsync<T>(string URL, object Data)
        {
            string response = string.Empty;
            try
            {

                string serializedUser = JsonConvert.SerializeObject(Data);
                StringContent content = new StringContent(serializedUser);
                var result = await _httpClient.PostAsync(URL, content);
                response = await result.Content.ReadAsStringAsync();
                result.EnsureSuccessStatusCode();
                var outPut = JsonConvert.DeserializeObject<T>(response);
                return outPut;
            }
            catch (Exception)
            {

                throw new Exception(response);
            }
        }
    }
}
