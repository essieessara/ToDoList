using System.Threading.Tasks;

namespace TodoList.Client.Helpers
{
    public interface ILocalStorage
    {
        ValueTask AddLocalStorageAsync<T>(string key, T Data);
        ValueTask<T> CallLocalStorageAsync<T>(string Key);
    }
}