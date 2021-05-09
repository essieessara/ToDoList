using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Helpers;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.ToDoItemsModels;
using ToDoList.Services.DataManagementService;
using ToDoList.Services.ToDoServices;

namespace ToDoList.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/ToDo")]
    public class ToDoController : ToDoControllerBase
    {
        private readonly IToDoItemService _service;
        private readonly IDataManagementService _data;
        public ToDoController(IToDoItemService service, IDataManagementService data)
        {
            _service = service;
            _data = data;
        }
        [HttpGet("GetAllMyTodo")]
        public Task<ActionResult<IEnumerable<ToDoItemtEntity>>> GetUserToDoListByIdAsync()
                    => TryCatch<IEnumerable<ToDoItemtEntity>>(async () =>
                    {

                        var ToDo = await _service.GetUserToDoListByIdAsync();
                        return Ok(ToDo);

                    });
        [HttpGet("GetMytodo/{id}")]
        public Task<ActionResult<ToDoItemtEntity>> GetListsAsync(int id)
           => TryCatch<ToDoItemtEntity>(async () =>
           {
               var output = await _service.GetByIdAsync(id);
               return Ok(output);
           });
        [HttpPut("ChangeName")]
        public Task<ActionResult> UpdateListItemAsync(UpdateTodoItemNameModel toDodb)
            => TryCatch(async () =>
            {
                var Item = await _service.UpdateToDoNameAsync(toDodb);
                return Ok(Item);
            });
        [HttpPut("MarkAsCompleted/{id}")]
        public Task<ActionResult> UpdateListItemStatusAsync(int id)
             => TryCatch(async () =>
             {
                 var Item = await _service.UpdateStatusAsync(id);
                 return Ok(Item);
             });
        [HttpPost("CreateToDo")]
        public Task<ActionResult<ToDoItemResponseModel>> PostToDodbAsync(CreateTodoItemModel toDodb)
               => TryCatch<ToDoItemResponseModel>(async () =>
               {
                   return Ok(await _data.CreateAsync(toDodb));
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

