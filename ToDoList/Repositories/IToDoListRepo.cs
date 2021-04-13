using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Repositories
{
    public interface IToDoListRepo
    {
        Task<ToDoListEntity> CreateToDoItemAsync(ToDoListEntity toDodb);
        Task DeleteToDoByIdAsync(int id);
        Task EditToDoByIdAsync(ToDoListEntity toDodb);
        Task<List<ToDoListEntity>> GetAllToDoListASync();
        Task<ToDoListEntity> GetToDoByIdAsync(int id);
        Task<ToDoListEntity> GetToDoByNameAsync(string name);
    }
}