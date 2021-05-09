using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Repositories.ToDoItemRepos
{
    public interface IToDoItemRepo
    {
        Task<ToDoItemtEntity> CreateToDoItemAsync(ToDoItemtEntity toDodb);
        Task DeleteToDoByIdAsync(int id);
        Task<ToDoItemtEntity> EditToDoByIdAsync(ToDoItemtEntity toDodb);
        Task<List<ToDoItemtEntity>> GetAllToDoListASync();
        Task<ToDoItemtEntity> GetToDoByIdAsync(int id, int uid);
        Task<ToDoItemtEntity> GetToDoByNameAsync(string name);
        Task<List<ToDoItemtEntity>> GetToDoUserByIdAsync(int id);
    }
}