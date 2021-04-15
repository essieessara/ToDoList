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
    public class ToDoUserController : ToDoControllerBase
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ToDoListController : ToDoControllerBase
        {
            private readonly IToDoUserService _service;

            public ToDoListController(IToDoUserService service)
                => _service = service;


            [HttpGet("GetUsers")]
            public Task<ActionResult<IEnumerable<ToDoUsersEntity>>> GetListsAsync()
                => TryCatch<IEnumerable<ToDoUsersEntity>>(async () => {
                    var ToDo = await _service.GetUserListAsync();
                    return Ok(ToDo);

                });
            [HttpGet("GetUserById/{id}")]
            public Task<ActionResult<ToDoUsersEntity>> GetListsAsync(int id)
                => TryCatch<ToDoUsersEntity>(async () => {
                    var output = await _service.GetUserByIdAsync(id);
                    return Ok(output);
                });
            [HttpPut("UpdateToDoUser")]
            public Task<ActionResult> UpdateListItemAsync(UpdateTodoUserModel User)
                => TryCatch(async () => {
                    await _service.UpdateToDoUserAsync(User);
                    return Ok();
                });
           
            [HttpPost("RegisterUser")]
            public Task<ActionResult<ToDoUsersEntity>> PostToDodbAsync(RegisterToDoUser User)
                   => TryCatch<ToDoUsersEntity>(async () =>
                   {
                       return Ok(await _service.RegisterUserAsync(User));
                   });
            [HttpDelete("DeleteUserAccount/{id}")]
            public Task<ActionResult> DeleteToDodbAsync(int id)
                  => TryCatch(async () =>
                  {
                      await _service.DeleteUserAccountAsync(id);
                      return Ok();
                  });

        }
    }
}
