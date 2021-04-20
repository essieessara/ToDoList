using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Repositories.UserRepos
{
    public interface IUserRepo
    {
        Task<UserEntity> CreateToDoUserAsync(UserEntity toDoUser);
        Task DeleteToDoUserByIdAsync(int id);
        Task EditToDoUserByIdAsync(UserEntity toDoUser);
        Task<List<UserEntity>> GetAllToDoUsersListASync();
        Task<UserEntity> GetToDoUserByIdAsync(int id);
        Task<UserEntity> GetToDoUserByUsernameAsync(string name);
    }
}