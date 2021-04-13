using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListService _service;

        public ToDoListController(IToDoListService service)
            => _service = service;
        

        [HttpGet("GetLists")]
        public async Task<ActionResult<IEnumerable<ToDoListEntity>>> GetListsAsync()
        {
            try
            {
                var ToDo = await _service.GetListAsync();
                return Ok(ToDo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpGet("GetListItem/{id}")]
        public async Task<ActionResult<ToDoListEntity>> GetListsAsync(int id)
        {
            try
            { 
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpPut("UpdateToDoItem/{id}")]
        public async Task<ActionResult<ToDoListEntity>> UpdateListItemAsync( UpdateTodoItemNameModel toDodb)
        {
            try
            {
                await _service.UpdateAsync(toDodb);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }
        [HttpPut("EndToDoItem/{id}")]
        public async Task<ActionResult<ToDoListEntity>> UpdateListItemStatusAsync(int id)
        {
            try
            {
                await _service.UpdateStatusAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }
        [HttpPost("CreateToDo")]
        public async Task<ActionResult<ToDoListEntity>> PostToDodbAsync(CreateTodoItemModel toDodb)
        {
            try
            {
                return Ok(await _service.CreateAsync(toDodb));
            }
            catch (ToDoExceptions e)
            {
                return NotFound(new ResponseError(e.Errors));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }



        }
        [HttpDelete("DeleteToDo/{id}")]
        public async Task<ActionResult> DeleteToDodbAsync(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (ToDoExceptions e)
            {
                return NotFound(new ResponseError(e.Errors));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

    }
}
