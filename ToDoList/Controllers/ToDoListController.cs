using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions;
using ToDoList.Models;
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

        [HttpGet("GetLists")]

        public async Task<IEnumerable<ToDoListEntity>> GetLists()
        {
            try 
            {
                var ToDo = await _service.GetListAsync();
                return ToDo;
            }
            catch (Exception e)
            {
                throw;
            }
            
        }


        [HttpGet("GetListItem/{id}")]
        public async Task<ToDoListEntity> GetLists(int id)
        {
            try
            {
                return await _service.GetById(id);
            }
            catch (Exception e)
            {
                throw;
            }

        }



        [HttpPut("UpdateToDoItem/{id}")]
        public async Task UpdateListItem( int id, UpdateTodoItemNameModel toDodb)
        {
            try
            {
                await _service.Update(id, toDodb);
            }
            catch (Exception e)
            {
                throw;
            }

            
        }

        [HttpPut("EndToDoItem/{id}")]
        public async Task UpdateListItemStatus(int id, UpdateTodoItemStatusModel toDodb)
        {
            try
            {
                await _service.UpdateStatus(id, toDodb);
            }
            catch (Exception e)
            {
                throw;
            }

            
        }


        [HttpPost("CreateToDo")]
        public async Task<ToDoListEntity> PostToDodb(CreateTodoItemModel toDodb)
        {
            try
            {
                return await _service.Create(toDodb);
            }
            catch (Exception e)
            {
                throw;
            }


            
        }


        [HttpDelete("DeleteToDo/{id}")]
        public async Task<ActionResult> DeleteToDodb(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch(ToDoExceptions e)
            {
                return NotFound( new ResponseError(e.Errors));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            
        }

    }
}
