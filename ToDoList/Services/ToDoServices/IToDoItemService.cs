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
        Task DeleteAsync(int id , int uid);
        Task<ToDoItemtEntity> GetByIdAsync(int id , int uid);
        Task<List<ToDoItemtEntity>> GetListAsync();
        Task<ToDoItemtEntity> UpdateToDoNameAsync(UpdateTodoItemNameModel toDodb);
        Task<ToDoItemtEntity> UpdateStatusAsync(int id , int uid);
        Task<List<ToDoItemtEntity>> GetUserToDoListByIdAsync(int id);
    }
}