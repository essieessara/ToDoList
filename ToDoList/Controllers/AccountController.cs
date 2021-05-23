using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using ToDoList.Database;
using ToDoList.Helpers;
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
        public Task<ActionResult<ResponseModel>> RegisterUserAsync(RegisterUserModel User)
              => TryCatch<UserEntity>(async () =>
              {
                  var user = await _service.RegisterUserAsync(User);
                  SucessResponseModel<UserEntity> response = new SucessResponseModel<UserEntity>();
                  response.Data = user;
                  return Ok(response);
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
                SucessResponseModel<UserEntity> response = new SucessResponseModel<UserEntity>();
                response.Data = user;
                return Ok(response);
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
