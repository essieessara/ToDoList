using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Repositories;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListService _service;

        public ToDoListController(IToDoListService service)
        {
            _service = service;
        }

        // GET: api/ToDoList
        [HttpGet]

        public async Task<ActionResult<IEnumerable<ToDoListEntity>>> GetLists()
        {
            return await _service.GetListAsync();
        }



        // GET: api/ToDoList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoListEntity>> GetListItemById(int id)
        {
            return await _service.GetById(id);
        }

        // PUT: api/ToDoList/5

        [HttpPut("{id}")]
        public async Task UpdateListItem(int id, ToDoListEntity toDodb)
        {
             await _service.Update(id, toDodb);
        }

        // POST: api/ToDoList

        [HttpPost]
        public async Task<ActionResult<ToDoListEntity>> PostToDodb(ToDoListEntity toDodb)
        {
            return await _service.Create(toDodb);
        }

        // DELETE: api/ToDoList/5
        [HttpDelete("{id}")]
        public async Task DeleteToDodb(int id)
        {
            await _service.DeleteAsync(id);
        }

    }
}
