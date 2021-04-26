using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models.UserModels;
using ToDoList.Services.AccountMangmentService;
using ToDoList.Services.UserServices;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ToDoControllerBase
    {
        private readonly IUserService _service;
        private readonly IAccountManagmentService _account;

        public UserController(IUserService service , IAccountManagmentService account)
        {
            _service = service;
            _account = account;
        }

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
                return Ok(await _account.GetUserByIdAsync(id));
            });
        [HttpPut("UpdateToDoUser")]
        public Task<ActionResult> UpdateUserInfoAsync(UpdateUserModel User)
            => TryCatch(async () =>
            {
                var user = await _service.UpdateToDoUserAsync(User);
                return Ok(user);
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
                  await _account.DeleteUserAccountAsync(id);
                  return Ok();
              });


    }
}
