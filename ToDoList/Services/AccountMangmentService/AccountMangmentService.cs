using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using ToDoList.Database;
using ToDoList.Models.ResponseModels;
using ToDoList.Services.ToDoServices;
using ToDoList.Services.UserServices;

namespace ToDoList.Services.AccountMangmentService
{
    public partial class AccountManagmentService : IAccountManagmentService
    {
        private readonly IUserService _userService;
        private readonly IToDoItemService _itemService;
        private readonly string _tokenKey;
        private readonly int _tokenExpireDays;

        public AccountManagmentService(IUserService userService,
            IToDoItemService itemService, IConfiguration configuration)
        {
            _userService = userService;
            _itemService = itemService;
            _tokenKey = configuration.GetSection("AuthToken:Key").Value;
            _tokenExpireDays = Convert.ToInt16(configuration.GetSection("AuthToken:ExpireDays").Value);
        }



        public Task DeleteUserAccountAsync(int id)
            => TryCatch(async () =>
            {

                var todoList = await _itemService.GetUserToDoListByIdAsync();

                foreach (var ToDo in todoList)
                {
                    await _itemService.DeleteAsync(ToDo.ItemID);
                }
                await _userService.DeleteUserAccountAsync(id);

            });
        public Task<UserDataResponseModel> GetUserByIdAsync(int id)
          => TryCatch(async () =>
          {
              var todoListUser = await _userService.GetUserByIdAsync(id);
              return todoListUser;

          });

        public Task<SuccesLogin> LoginUserAsync(LoginUserModel toDoUser)
             => TryCatch(async () =>
             {
                 UserEntity todoUser = await _userService.CheckUserAsync(toDoUser);
                 byte[] tokenBytes = Encoding.ASCII.GetBytes(_tokenKey);

                 SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor();
                 tokenDescriptor.Expires = DateTime.Now.AddDays(_tokenExpireDays);

                 List<Claim> claims = new List<Claim>();
                 claims.Add(new Claim(ClaimTypes.Sid, todoUser.UserID.ToString()));
                 claims.Add(new Claim(ClaimTypes.GivenName, todoUser.Username));

                 tokenDescriptor.Subject = new ClaimsIdentity(claims);

                 tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenBytes),
                     SecurityAlgorithms.HmacSha512Signature);


                 System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler tokenHandler = new();
                 SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                 SuccesLogin tokenString = new SuccesLogin { TokenString = tokenHandler.WriteToken(token) };
                 return tokenString;

             });

    }
}
