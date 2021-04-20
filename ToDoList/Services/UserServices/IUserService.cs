using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.UserModels;

namespace ToDoList.Services.UserServices
{
    public interface IUserService
    {
        Task DeleteUserAccountAsync(int id);
        Task<UserDataResponseModel> GetUserByIdAsync(int id);
        Task<List<UserEntity>> GetUserListAsync();
        Task<UserEntity> RegisterUserAsync(RegisterUser toDoUser);
        Task UpdateToDoUserAsync(UpdateUserModel User);
    }
}