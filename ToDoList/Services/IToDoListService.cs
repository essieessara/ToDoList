using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models;

namespace ToDoList.Services
{
    public interface IToDoListService
    {
        Task<ToDoListEntity> Create(CreateTodoItemModel toDodb);
        Task DeleteAsync(int id);
        Task<ToDoListEntity> GetById(int id);
        Task<List<ToDoListEntity>> GetListAsync();
        Task Update(int id, UpdateTodoItemNameModel toDodb);
        Task UpdateStatus(int id, UpdateTodoItemStatusModel toDodb);
    }
}