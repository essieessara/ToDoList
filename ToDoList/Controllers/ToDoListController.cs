using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Repositories;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly ToDoListContext _context;

        public ToDoListController(ToDoListContext context)
        {
            _context = context;
        }

        // GET: api/ToDoList
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<ToDoListEntity>>> GetLists()
        //{
        //    return await _context.Lists.ToListAsync();
        //}

        // GET: api/ToDoList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoListEntity>> GetToDodb(int id)
        {
            var toDodb = await _context.Lists.FindAsync(id);

            if (toDodb == null)
            {
                return NotFound();
            }

            return toDodb;
        }

        // PUT: api/ToDoList/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDodb(int id, ToDoListEntity toDodb)
        {
            if (id != toDodb.ItemID)
            {
                return BadRequest();
            }

            _context.Entry(toDodb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDodbExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ToDoList

        [HttpPost]
        public async Task<ActionResult<ToDoListEntity>> PostToDodb(ToDoListEntity toDodb)
        {
            _context.Lists.Add(toDodb);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDodb", new { id = toDodb.ItemID }, toDodb);
        }

        // DELETE: api/ToDoList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDodb(int id)
        {
            var toDodb = await _context.Lists.FindAsync(id);
            if (toDodb == null)
            {
                return NotFound();
            }

            _context.Lists.Remove(toDodb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDodbExists(int id)
        {
            return _context.Lists.Any(e => e.ItemID == id);
        }
    }
}
