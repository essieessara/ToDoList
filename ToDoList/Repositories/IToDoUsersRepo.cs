using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Repositories
{
    public interface IToDoUsersRepo
    {
        Task<ToDoUsersEntity> CreateToDoUserAsync(ToDoUsersEntity toDoUser);
        Task DeleteToDoUserByIdAsync(int id);
        Task EditToDoUserByIdAsync(ToDoUsersEntity toDoUser);
        Task<List<ToDoUsersEntity>> GetAllToDoUsersListASync();
        Task<ToDoUsersEntity> GetToDoUserByIdAsync(int id);
    }
}