using System.Threading.Tasks;

namespace TodoList.Client.Services.HttpClientServ
{
    public interface IHttpClientService
    {
        Task<T> PostAsync<T>(string URL, object Data);
    }
}