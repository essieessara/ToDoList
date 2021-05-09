using System.Threading.Tasks;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.UserModels;

namespace ToDoList.Services.AccountMangmentService
{
    public interface IAccountManagmentService
    {
        Task DeleteUserAccountAsync(int id);
        Task<UserDataResponseModel> GetUserByIdAsync(int id);
        Task<string> LoginUserAsync(LoginUserModel toDoUser);

    }
}