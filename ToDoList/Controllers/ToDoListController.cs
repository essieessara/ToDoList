using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
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
            var ToDo= await _service.GetListAsync();
            return ToDo;
        }


        [HttpGet("GetListItem/{id}")]
        public async Task<ToDoListEntity> GetLists(int id)
        {
            return await _service.GetById(id);
        }



        [HttpPut("UpdateToDoItem/{id}")]
        public async Task UpdateListItem( int id, UpdateTodoItemNameModel toDodb)
        { 
             await _service.Update(id, toDodb);
        }

        [HttpPut("EndToDoItem/{id}")]
        public async Task UpdateListItemStatus(int id, UpdateTodoItemStatusModel toDodb)
        {
            await _service.UpdateStatus(id, toDodb);
        }


        [HttpPost("CreateToDo")]
        public async Task<ToDoListEntity> PostToDodb(CreateTodoItemModel toDodb)
        {
 
            return await _service.Create(toDodb);
        }


        [HttpDelete("DeleteToDo/{id}")]
        public async Task DeleteToDodb(int id)
        {
            await _service.DeleteAsync(id);
        }

    }
}
