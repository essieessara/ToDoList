using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using ToDoList.Database;
using ToDoList.Helpers.Mapper;
using ToDoList.Models.ResponseModels;

using ToDoList.Repositories.UserRepos;

namespace ToDoList.Services.UserServices
{
    public partial class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        private readonly UserMapper _mapper;
        private readonly ClaimsPrincipal _loggedUser;

        public UserService(IUserRepo User, IHttpContextAccessor contextAccessor)
        {
            _repo = User;
            _mapper = new();
            _loggedUser = contextAccessor.HttpContext.User;
        }

        public Task<List<UserEntity>> GetUserListAsync()
             => TryCatch(async () =>
             {
                 var todoUserList = await _repo.GetAllToDoUsersListASync();
                 return todoUserList;
             });
        public Task<UserDataResponseModel> GetUserByIdAsync(int id)
            => TryCatch(async () =>
            {
                UserEntity todoListUser = await _repo.GetUserByIdAsync(id);

                ValidateGetUserById(todoListUser);

                var output = _mapper.Map(todoListUser);

                //todoListUser.Lists = await _repo.GetUserByIdAsync(id);

                output.ToDoLists = todoListUser.Lists?.Select(x => new ToDoItemResponseModel
                {
                    CreatedDate = x.CreatedDate,
                    EndedDate = x.EndedDate,
                    IsFinished = x.IsFinished,
                    ItemID = x.ItemID,
                    ItemName = x.ItemName
                }).ToList();
                return output;

            });
        public Task DeleteUserAccountAsync(int id)
            => TryCatch(async () =>
            {
                var objectTovalidate = await _repo.GetUserByIdAsync(id);
                ValidateDeleteUserById(objectTovalidate);
                await _repo.DeleteToDoUserByIdAsync(id);

            });
        public Task<UserEntity> RegisterUserAsync(RegisterUserModel toDoUser)
             => TryCatch(async () =>
             {
                 var dbExistingModel = await GetByUsernameAsync(toDoUser.Username);
                 UserEntity dbCreateUser = _mapper.Map(toDoUser);

                 ValidateRegister(toDoUser, dbExistingModel);

                 var todoNewUser = await _repo.CreateToDoUserAsync(dbCreateUser);
                 return todoNewUser;

             });
        public Task<UserEntity> CheckUserAsync(LoginUserModel toDoUser)
             => TryCatch(async () =>
             {
                 var dbExistingModel = await GetByUsernameAsync(toDoUser.Username);
                 UserEntity User = _mapper.Map(toDoUser);

                 ValidateLogin(toDoUser, dbExistingModel);

                 var todoUser = await _repo.GetToDoUserByUsernameAsync(User.Username);

                 return todoUser;

             });
        public Task<UserEntity> UpdateToDoUserAsync(UpdateUserModel User)
             => TryCatch(async () =>
             {
                 Validateauthentication();
                 Claim userClaim = _loggedUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
                 ValidateLogin(userClaim);

                 int userId = Convert.ToInt32(userClaim.Value);
                 UserEntity dbUpdateModel = await _repo.GetUserByIdAsync(userId);
                 var dbExistingModel = await GetByUsernameAsync(User.Username);

                 ValidateUpdate(dbUpdateModel);

                 dbUpdateModel.UserID = userId;
                 dbUpdateModel.FirstName = User.FirstName;
                 dbUpdateModel.LastName = User.LastName;

                 ValidateUpdateUsername(dbExistingModel);

                 dbUpdateModel.Username = User.Username;

                 ValidateUpdatePass(dbUpdateModel, User);

                 dbUpdateModel.Password = User.NewPassword;

                 var UpdatedUser = await _repo.EditToDoUserByIdAsync(dbUpdateModel);
                 return UpdatedUser;

             });
        public Task<UserEntity> ResetPasswordAsync(ResetPasswordModel User)
            => TryCatch(async () =>
            {
                Validateauthentication();
                Claim userClaim = _loggedUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
                ValidateLogin(userClaim);

                string username = userClaim.Value;
                UserEntity dbUpdateModel = await _repo.GetToDoUserByUsernameAsync(username);

                ValidateUpdatePass(dbUpdateModel, User);

                dbUpdateModel.Password = User.NewPassword;

                var UpdatedUser = await _repo.EditToDoUserByIdAsync(dbUpdateModel);
                return UpdatedUser;

            });

        private async Task<UserEntity> GetByUsernameAsync(string name)
           => await TryCatch(async () =>
           {
               var todoListItem = await _repo.GetToDoUserByUsernameAsync(name);
               return todoListItem;
           });
        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }


    }
}
