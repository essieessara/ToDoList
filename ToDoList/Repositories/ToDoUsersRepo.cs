using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Repositories
{
    public class ToDoUsersRepo : IToDoUsersRepo
    {
        private readonly ToDoListContext _context;

        public ToDoUsersRepo(ToDoListContext context)
         => _context = context;

        public async Task<List<ToDoUsersEntity>> GetAllToDoUsersListASync()
            => await _context.Users.ToListAsync();

        public async Task<ToDoUsersEntity> GetToDoUserByIdAsync(int id)
            => await _context.Users.FirstOrDefaultAsync(x => x.UserID == id);
        public async Task<ToDoUsersEntity> GetToDoUserByUsernameAsync(string name)
           => await _context.Users.FirstOrDefaultAsync(x => x.Username == name);

        public async Task<ToDoUsersEntity> CreateToDoUserAsync(ToDoUsersEntity toDoUser)
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

        public async Task EditToDoUserByIdAsync(ToDoUsersEntity toDoUser)
        {
            _context.Entry(toDoUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
