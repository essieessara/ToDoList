using System.Net.Http;
using System.Threading.Tasks;

namespace TodoList.Client.Services.ApiClient
{
    public interface IApiClient
    {
        Task<T> PostAsync<T>(string URL, object Data);
        Task<T> PutAsync<T>(string URL, object Data);
        Task<HttpResponseMessage> GetAsync(string URL);
    }
}