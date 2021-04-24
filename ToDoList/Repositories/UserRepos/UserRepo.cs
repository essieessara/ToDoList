using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Repositories.UserRepos
{
    public class UserRepo : IUserRepo
    {
        private readonly ToDoListContext _context;

        public UserRepo(ToDoListContext context)
         => _context = context;

        public async Task<List<UserEntity>> GetAllToDoUsersListASync()
            => await _context.Users.ToListAsync();

        public async Task<UserEntity> GetUserByIdAsync(int id)
            => await _context.Users.FirstOrDefaultAsync(x => x.UserID == id);
        public async Task<UserEntity> GetToDoUserByUsernameAsync(string name)
           => await _context.Users.FirstOrDefaultAsync(x => x.Username == name);

        public async Task<UserEntity> CreateToDoUserAsync(UserEntity toDoUser)
        {
            _context.Users.Add(toDoUser);
            await _context.SaveChangesAsync();
            return toDoUser;
        }

        public async Task DeleteToDoUserByIdAsync(int id)
        {
            var toDoUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(toDoUser);
            await _context.SaveChangesAsync();
        }

        public async Task EditToDoUserByIdAsync(UserEntity toDoUser)
        {
            _context.Entry(toDoUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
