using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using ToDoList.Models.ResponseModels;


namespace ToDoList.Services.AccountMangmentService
{
    public interface IAccountManagmentService
    {
        Task DeleteUserAccountAsync(int id);
        Task<UserDataResponseModel> GetUserByIdAsync(int id);
        Task<SuccesLogin> LoginUserAsync(LoginUserModel toDoUser);

    }
}