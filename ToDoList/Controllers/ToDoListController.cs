﻿using Microsoft.AspNetCore.Mvc;
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
            catch (Exception)
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
            catch (Exception)
            {
                throw;
            }

        }



        [HttpPut("UpdateToDoItem/{id}")]
        public async Task UpdateListItem(int id, UpdateTodoItemNameModel toDodb)
        {
            try
            {
                await _service.Update(id, toDodb);
            }
            catch (Exception)
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
            catch (Exception)
            {
                throw;
            }


        }


        [HttpPost("CreateToDo")]
        public async Task<ActionResult<ToDoListEntity>> PostToDodb(CreateTodoItemModel toDodb)
        {
            try
            {
                return Ok(await _service.Create(toDodb.ItemName, toDodb));
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
        public async Task<ActionResult> DeleteToDodb(int id)
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
