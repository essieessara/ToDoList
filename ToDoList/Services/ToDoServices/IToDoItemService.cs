using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.ToDoItemsModels;

namespace ToDoList.Services.ToDoServices
{
    public interface IToDoItemService
    {
        Task<ToDoItemResponseModel> CreateAsync(CreateTodoItemModel toDodb);
        Task DeleteAsync(int id);
        Task<ToDoItemtEntity> GetByIdAsync(int id);
        Task<List<ToDoItemtEntity>> GetListAsync();
        Task<ToDoItemtEntity> UpdateToDoNameAsync(UpdateTodoItemNameModel toDodb);
        Task<ToDoItemtEntity> UpdateStatusAsync(int id);
        Task<List<ToDoItemtEntity>> GetListOfUserByIdAsync(int id);
    }
}