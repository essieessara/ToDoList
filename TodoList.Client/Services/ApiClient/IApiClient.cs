using System.Net.Http;
using System.Threading.Tasks;

namespace TodoList.Client.Services.ApiClient
{
    public interface IApiClient
    {
        Task<T> PostAsync<T>(string URL, object Data);
        Task<string> PostAsync(string URL, object Data);
    }
}