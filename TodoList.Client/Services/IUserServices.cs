using System.Net.Http;
using System.Threading.Tasks;
using TodoList.Client.Models;

namespace TodoList.Client.Services
{
    public interface IUserServices
    {
        HttpClient _httpClient { get; }

        Task<User> LoginAsync(User user);
    }
}