using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models.UserModels;
using ToDoList.Services.UserServices;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ToDoControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
            => _service = service;


        [HttpGet("GetUsers")]
        public Task<ActionResult<IEnumerable<UserEntity>>> GetUsersListsAsync()
            => TryCatch<IEnumerable<UserEntity>>(async () =>
            {
                var User = await _service.GetUserListAsync();
                return Ok(User);

            });
        [HttpGet("GetUserById/{id}")]
        public Task<ActionResult<UserEntity>> GetUserListsAsync(int id)
            => TryCatch<UserEntity>(async () =>
            {
                return Ok(await _service.GetUserByIdAsync(id));
            });
        [HttpPut("UpdateToDoUser")]
        public Task<ActionResult> UpdateUserInfoAsync(UpdateUserModel User)
            => TryCatch(async () =>
            {
                await _service.UpdateToDoUserAsync(User);
                return Ok();
            });

        [HttpPost("RegisterUser")]
        public Task<ActionResult<UserEntity>> RegisterUserAsync(RegisterUser User)
               => TryCatch<UserEntity>(async () =>
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
