using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IToDoUserService
    {
        Task DeleteUserAccountAsync(int id);
        Task<ToDoUsersEntity> GetUserByIdAsync(int id);
        Task<List<ToDoUsersEntity>> GetUserListAsync();
        Task<ToDoUsersEntity> RegisterUserAsync(RegisterToDoUser toDoUser);
        Task UpdateToDoUserAsync(UpdateTodoUserModel User);
    }
}