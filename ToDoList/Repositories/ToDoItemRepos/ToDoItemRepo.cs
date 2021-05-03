using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Repositories.ToDoItemRepos
{

    public class ToDoItemRepo : IToDoItemRepo
    {
        private readonly ToDoListContext _context;

        public ToDoItemRepo(ToDoListContext context)
            => _context = context;


        public async Task<List<ToDoItemtEntity>> GetAllToDoListASync()
            => await _context.Lists.ToListAsync();

        public async Task<ToDoItemtEntity> GetToDoByIdAsync(int id, int uid)
        { 
            var todo = await _context.Lists.FirstOrDefaultAsync(x=>x.ItemID == id && x.UserID == uid);
            return todo;
        }
        public async Task<List<ToDoItemtEntity>> GetToDoUserByIdAsync(int id)
           => await _context.Lists.Where(x => x.UserID == id).ToListAsync();
        public async Task<ToDoItemtEntity> GetToDoByNameAsync(string name)
            => await _context.Lists.FirstOrDefaultAsync(x => x.ItemName == name);

        public async Task<ToDoItemtEntity> CreateToDoItemAsync(ToDoItemtEntity toDodb)
        {
            _context.Lists.Add(toDodb);
            await _context.SaveChangesAsync();

            return toDodb;
        }

        public async Task DeleteToDoByIdAsync(int id)
        {
            var toDo = await _context.Lists.FindAsync(id);
            _context.Lists.Remove(toDo);
            await _context.SaveChangesAsync();

        }

        public async Task<ToDoItemtEntity> EditToDoByIdAsync(ToDoItemtEntity toDodb)
        {

            _context.Entry(toDodb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return toDodb;

        }

    }
}

