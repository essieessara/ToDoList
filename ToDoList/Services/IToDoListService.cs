using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IToDoListService
    {
        Task<ToDoListEntity> CreateAsync( CreateTodoItemModel toDodb);
        Task DeleteAsync(int id);
        Task<ToDoListEntity> GetByIdAsync(int id);
        Task<List<ToDoListEntity>> GetListAsync();
        Task UpdateAsync( UpdateTodoItemNameModel toDodb);
        Task UpdateStatusAsync(int id);
    }
}