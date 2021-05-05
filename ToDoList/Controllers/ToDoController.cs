using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.Database;
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
                        var userID = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
                        var ToDo = await _service.GetUserToDoListByIdAsync(Convert.ToInt32(userID.Value));
                        return Ok(ToDo);

                    });
        [HttpGet("GetMytodo/{id}")]
        public Task<ActionResult<ToDoItemtEntity>> GetListsAsync(int id , int uid)
           => TryCatch<ToDoItemtEntity>(async () =>
           {
               var output = await _service.GetByIdAsync(id , uid);
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
        public Task<ActionResult> UpdateListItemStatusAsync(int id , int uid)
             => TryCatch(async () =>
             {
                 var Item = await _service.UpdateStatusAsync(id , uid);
                 return Ok(Item);
             });
        [HttpPost("CreateToDo")]
        public Task<ActionResult<ToDoItemResponseModel>> PostToDodbAsync(CreateTodoItemModel toDodb)
               => TryCatch<ToDoItemResponseModel>(async () =>
               {
                   return Ok(await _data.CreateAsync(toDodb));
               });
        [HttpDelete("DeleteToDo/{id}")]
        public Task<ActionResult> DeleteToDodbAsync(int id , int uid)
                 => TryCatch(async () =>
                 {
                     await _service.DeleteAsync(id , uid);
                     return Ok();
                 });

    }

}

