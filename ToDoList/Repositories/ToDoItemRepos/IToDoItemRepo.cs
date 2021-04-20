using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Repositories.ToDoItemRepos
{
    public interface IToDoItemRepo
    {
        Task<ToDoItemtEntity> CreateToDoItemAsync(ToDoItemtEntity toDodb);
        Task DeleteToDoByIdAsync(int id);
        Task EditToDoByIdAsync(ToDoItemtEntity toDodb);
        Task<List<ToDoItemtEntity>> GetAllToDoListASync();
        Task<ToDoItemtEntity> GetToDoByIdAsync(int id);
        Task<ToDoItemtEntity> GetToDoByNameAsync(string name);
        Task<List<ToDoItemtEntity>> GetToDoUserByIdAsync(int id);
    }
}