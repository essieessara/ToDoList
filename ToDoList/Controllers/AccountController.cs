using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions.UserExceptions;
using ToDoList.Helpers;
using ToDoList.Models.UserModels;
using ToDoList.Services.AccountMangmentService;
using ToDoList.Services.UserServices;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/Account")]


    public class AccountController : ToDoControllerBase
    {

        private readonly IUserService _service;
        private readonly IAccountManagmentService _account;

        public AccountController(IUserService service, IAccountManagmentService account)
        {
            _service = service;
            _account = account;
        }

        [HttpPost("Register")]
        public Task<ActionResult<UserEntity>> RegisterUserAsync(RegisterUserModel User)
              => TryCatch<UserEntity>(async () =>
              {
                  return Ok(await _service.RegisterUserAsync(User));
              });
        [HttpPost("Login")]
        public Task<ActionResult> LoginUserAsync(LoginUserModel User)
              => TryCatch(async () =>
              {
                  return Ok(await _account.LoginUserAsync(User));
              });
        [Authorize]
        [HttpPut("ResetPassword")]
        public Task<ActionResult> ResetPassword(ResetPasswordModel model)
            => TryCatch(async () =>
            {
                var user = await _service.ResetPasswordAsync(model);
                return Ok(user);
            });  
        [Authorize]
        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            await _service.SignOutAsync();
            return Ok();
        }
    }
}
