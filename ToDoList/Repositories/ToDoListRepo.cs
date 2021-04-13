using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Repositories
{

    public class ToDoListRepo : IToDoListRepo
    {
        private readonly ToDoListContext _context;

        public ToDoListRepo(ToDoListContext context)
        {
            _context = context;
        }

        public async Task<List<ToDoListEntity>> GetAllToDoList()
        {
            return await _context.Lists.ToListAsync();
        }


        public async Task<ToDoListEntity> GetToDoById(int id)
        {
            var toDoList = await _context.Lists.FirstOrDefaultAsync(x => x.ItemID == id);

            return toDoList;
        }

        public async Task<ToDoListEntity> GetToDoByName(string name)
        {
            var toDoList = await _context.Lists.FirstOrDefaultAsync(x => x.ItemName == name);

            return toDoList;
        }


        public async Task<ToDoListEntity> CreateToDoItem(ToDoListEntity toDodb)
        {
            _context.Lists.Add(toDodb);
            await _context.SaveChangesAsync();

            return toDodb;
        }

        public async Task DeleteToDoById(int id)
        {
            var toDo = await _context.Lists.FindAsync(id);
            _context.Lists.Remove(toDo);
            await _context.SaveChangesAsync();

        }

        public async Task EditToDoById(ToDoListEntity toDodb)
        {

            _context.Entry(toDodb).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}

