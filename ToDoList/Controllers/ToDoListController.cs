using Microsoft.AspNetCore.Authorization;
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
    public class ToDoListController : ToDoListControllerBase
    {
        private readonly IToDoListService _service;

        public ToDoListController(IToDoListService service)
            => _service = service;


        [HttpGet("GetLists")]
        public Task<ActionResult<IEnumerable<ToDoListEntity>>> GetListsAsync()
            => TryCatch<IEnumerable<ToDoListEntity>>(async () => {
                var ToDo = await _service.GetListAsync();
                return Ok(ToDo);

            });
        [HttpGet("GetListItem/{id}")]
        public Task<ActionResult<ToDoListEntity>> GetListsAsync(int id)
            => TryCatch<ToDoListEntity>(async () => {
                var output = await _service.GetByIdAsync(id);
                return Ok(output);
            });
        [HttpPut("UpdateToDoItem")]
        public Task<ActionResult> UpdateListItemAsync(UpdateTodoItemNameModel toDodb)
            => TryCatch(async () => {
                await _service.UpdateToDoNameAsync(toDodb);
                return Ok();
            });
        [HttpPut("EndToDoItem/{id}")]
        public Task<ActionResult> UpdateListItemStatusAsync(int id)
             => TryCatch(async () => {
               await _service.UpdateStatusAsync(id);
               return Ok();
             });       
        [HttpPost("CreateToDo")]
        public Task<ActionResult<ToDoListEntity>> PostToDodbAsync(CreateTodoItemModel toDodb)
               =>  TryCatch<ToDoListEntity>(async () =>
               {
                   return Ok(await _service.CreateAsync(toDodb));
               });   
        [HttpDelete("DeleteToDo/{id}")]
        public Task<ActionResult> DeleteToDodbAsync(int id)
              => TryCatch(async () =>
              {
                  await _service.DeleteAsync(id);
                  return Ok();
              });

    }

}

