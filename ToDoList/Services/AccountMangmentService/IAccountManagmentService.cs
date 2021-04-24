using System.Threading.Tasks;

namespace ToDoList.Services.AccountMangmentService
{
    public interface IAccountManagmentService
    {
        Task DeleteUserAccountAsync(int id);
    }
}