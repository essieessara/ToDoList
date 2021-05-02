using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models.UserModels;
using ToDoList.Services.AccountMangmentService;
using ToDoList.Services.UserServices;

namespace ToDoList.Controllers
{
    [Route("api/Accounts")]
    [ApiController]
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
        [HttpGet("Login")]
        public Task<ActionResult<UserEntity>> LoginUserAsync(LoginUserModel User)
              => TryCatch<UserEntity>(async () =>
              {
                  return Ok(await _service.LoginUserAsync(User));
              });

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await _service.SignOutAsync();
            return Ok();
        }
    }
}
