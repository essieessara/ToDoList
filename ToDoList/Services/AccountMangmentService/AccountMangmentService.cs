using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Mapper;
using ToDoList.Models.ResponseModels;
using ToDoList.Services.ToDoServices;
using ToDoList.Services.UserServices;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Models.UserModels;
using Microsoft.Extensions.Configuration;
using ToDoList.Exceptions.UserExceptions;

namespace ToDoList.Services.AccountMangmentService
{
    public partial class AccountManagmentService : IAccountManagmentService
    {
        private readonly IUserService _userService;
        private readonly IToDoItemService _itemService;
        private readonly string _tokenKey;
        private readonly int _tokenExpireDays;

        public AccountManagmentService(IUserService userService, IToDoItemService itemService, IConfiguration configuration)
        {
            _userService = userService;
            _itemService = itemService;
            _tokenKey = configuration.GetSection("AuthToken:Key").Value;
           _tokenExpireDays = Convert.ToInt16(configuration.GetSection("AuthToken:ExpireDays").Value);
        }



        public Task DeleteUserAccountAsync(int id)
            => TryCatch(async () =>
            {

                var todoList = await _itemService.GetListOfUserByIdAsync(id);

                foreach (var ToDo in todoList)
                {
                    await _itemService.DeleteAsync(ToDo.ItemID , ToDo.UserID);
                }
                await _userService.DeleteUserAccountAsync(id);

            });
        public Task<UserDataResponseModel> GetUserByIdAsync(int id)
          => TryCatch(async () =>
          {
              var todoListUser = await _userService.GetUserByIdAsync(id);
              return todoListUser;

          });

        public Task<string> LoginUserAsync(LoginUserModel toDoUser)
             => TryCatch(async () =>
             {
                 UserEntity todoUser = await _userService.CheckUserAsync(toDoUser);
                 byte[] tokenBytes = Encoding.ASCII.GetBytes(_tokenKey);

                 SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor();
                 tokenDescriptor.Expires = DateTime.Now.AddDays(_tokenExpireDays);

                 List<Claim> claims = new List<Claim>();
                 claims.Add(new Claim(JwtRegisteredClaimNames.NameId, todoUser.UserID.ToString()));
                 claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, todoUser.FullName));

                 tokenDescriptor.Subject = new ClaimsIdentity(claims);

                 tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenBytes),
                     SecurityAlgorithms.HmacSha512Signature);


                 System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler tokenHandler = new();
                 SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                 string tokenString = tokenHandler.WriteToken(token);
                 return tokenString;

             });

    }
}
