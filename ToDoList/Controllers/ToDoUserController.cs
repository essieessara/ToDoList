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
    public class ToDoUserController : ToDoControllerBase
    {
            private readonly IToDoUserService _service;

            public ToDoUserController(IToDoUserService service)
                => _service = service;


            [HttpGet("GetUsers")]
            public Task<ActionResult<IEnumerable<ToDoUsersEntity>>> GetUsersListsAsync()
                => TryCatch<IEnumerable<ToDoUsersEntity>>(async () => {
                    var User = await _service.GetUserListAsync();
                    return Ok(User);

                });
            [HttpGet("GetUserById/{id}")]
            public Task<ActionResult<ToDoUsersEntity>> GetUserListsAsync(int id)
                => TryCatch<ToDoUsersEntity>(async () => {
                    return Ok(await _service.GetUserByIdAsync(id));
                });
            [HttpPut("UpdateToDoUser")]
            public Task<ActionResult> UpdateUserInfoAsync(UpdateTodoUserModel User)
                => TryCatch(async () => {
                    await _service.UpdateToDoUserAsync(User);
                    return Ok();
                });
           
            [HttpPost("RegisterUser")]
            public Task<ActionResult<ToDoUsersEntity>> RegisterUserAsync(RegisterToDoUser User)
                   => TryCatch<ToDoUsersEntity>(async () =>
                   {
                       return Ok(await _service.RegisterUserAsync(User));
                   });
            [HttpDelete("DeleteUserAccount/{id}")]
            public Task<ActionResult> DeleteToDoUserAsync(int id)
                  => TryCatch(async () =>
                  {
                      await _service.DeleteUserAccountAsync(id);
                      return Ok();
                  });

        
    }
}
