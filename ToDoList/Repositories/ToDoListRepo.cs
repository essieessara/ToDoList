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
        => _context = context;
        

        public async Task<List<ToDoListEntity>> GetAllToDoListASync()
            =>await _context.Lists.ToListAsync();
        


        public async Task<ToDoListEntity> GetToDoByIdAsync(int id)
            => await _context.Lists.FirstOrDefaultAsync(x => x.ItemID == id);

           

        public async Task<ToDoListEntity> GetToDoByNameAsync(string name)
            =>  await _context.Lists.FirstOrDefaultAsync(x => x.ItemName == name);

         


        public async Task<ToDoListEntity> CreateToDoItemAsync(ToDoListEntity toDodb)
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

        public async Task EditToDoByIdAsync(ToDoListEntity toDodb)
        {

            _context.Entry(toDodb).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}

