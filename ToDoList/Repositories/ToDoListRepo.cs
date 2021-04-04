using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Repositories;

namespace ToDoList.Repositories
{
    // interface
    public class ToDoListRepo
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

        public async Task<ToDoListEntity> EditToDoById(int id, ToDoListEntity toDodb)
        {

            if (id == toDodb.ItemID)
           try {
                    _context.Entry(toDodb).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

            catch (DbUpdateConcurrencyException)
            {
                if (!ToDodbExists(id))
                {
                        return null;
                }
                else
                {
                    throw;
                }
            }

            return toDodb;
        }

        public async Task<ToDoListEntity> CreateToDodb(ToDoListEntity toDodb)
        {
             _context.Lists.Add(toDodb);
             await _context.SaveChangesAsync();

            return toDodb ;
        }

        public async Task DeleteToDoById(int id)
        {
            var toDo = await _context.Lists.FindAsync(id);
            if (toDo != null)
            {
                _context.Lists.Remove(toDo);
                await _context.SaveChangesAsync();
            }
        }

        private bool ToDodbExists(int id)
        {
            return _context.Lists.Any(e => e.ItemID == id);
        }

    }
}

