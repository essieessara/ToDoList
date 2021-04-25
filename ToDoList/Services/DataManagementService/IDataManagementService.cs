using System.Threading.Tasks;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.ToDoItemsModels;

namespace ToDoList.Services.DataManagementService
{
    public interface IDataManagementService
    {
        Task<ToDoItemResponseModel> CreateAsync(CreateTodoItemModel toDodb);
    }
}