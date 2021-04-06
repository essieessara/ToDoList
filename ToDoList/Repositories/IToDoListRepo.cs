using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Repositories
{
    public interface IToDoListRepo
    {
        Task<ToDoListEntity> CreateToDoItem(ToDoListEntity toDodb);
        Task DeleteToDoById(int id);
        Task EditToDoById(ToDoListEntity toDodb);
        Task<List<ToDoListEntity>> GetAllToDoList();
        Task<ToDoListEntity> GetToDoById(int id);
        Task<ToDoListEntity> GetToDoByName(string name);
    }
}