using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Services
{
    public interface IToDoListService
    {
        Task<ToDoListEntity> Create(ToDoListEntity toDodb);
        Task DeleteAsync(int id);
        Task<ToDoListEntity> GetById(int id);
        Task<List<ToDoListEntity>> GetListAsync();
        Task Update(int id, ToDoListEntity toDodb);
    }
}