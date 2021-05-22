using System.Net.Http;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;

namespace TodoList.Client.Services.AccountManagementServ
{
    public interface IAccountManagementService
    {
        Task<SuccesLogin> LoginAsync(LoginUserModel model);
        Task<string> SignupAsync(RegisterUserModel model);
    }
}