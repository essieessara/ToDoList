using System.Collections.Generic;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using ToDoList.Database;
using ToDoList.Models.ResponseModels;

namespace ToDoList.Services.UserServices
{
    public interface IUserService
    {
        Task DeleteUserAccountAsync(int id);
        Task<UserDataResponseModel> GetUserByIdAsync(int id);
        Task<List<UserEntity>> GetUserListAsync();
        Task<UserEntity> RegisterUserAsync(RegisterUserModel toDoUser);
        Task<UserEntity> UpdateToDoUserAsync(UpdateUserModel User);
        Task<UserEntity> CheckUserAsync(LoginUserModel toDoUser);
        Task<UserEntity> ResetPasswordAsync(ResetPasswordModel User);
        Task SignOutAsync();
    }
}