using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions.UserExceptions;
using ToDoList.Mapper;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.UserModels;
using ToDoList.Repositories.UserRepos;
using ToDoList.Services.ToDoServices;

namespace ToDoList.Services.UserServices
{
    public partial class UserService : IUserService
    {
        private readonly IUserRepo _repo;
       // private readonly IToDoItemService _itemService;
        private readonly UserMapper _mapper;

        public UserService(IUserRepo User)
        {
            _repo = User;
          //  _itemService = itemService;
            _mapper = new();
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

                    todoListUser.Lists = (ICollection<ToDoItemtEntity>)await _repo.GetUserByIdAsync(id);

                    output.ToDoLists = todoListUser.Lists.Select(x => new ToDoItemResponseModel
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

        public Task<UserEntity> RegisterUserAsync(RegisterUser toDoUser)
             => TryCatch(async () =>
             {
                 var dbExistingModel = await GetByUsernameAsync(toDoUser.Username);
                     UserEntity dbCreateUser = _mapper.Map(toDoUser);

                 ValidateRegister(toDoUser, dbExistingModel);

                 var todoNewUser = await _repo.CreateToDoUserAsync(dbCreateUser);
                  return todoNewUser;

             });

        public Task UpdateToDoUserAsync(UpdateUserModel User)
             => TryCatch(async () =>
             {
                 UserEntity dbUpdateModel = await _repo.GetUserByIdAsync(User.UserID);

                 ValidateUpdate(dbUpdateModel);

                dbUpdateModel.UserID = User.UserID;
                dbUpdateModel.FirstName = User.FirstName;
                dbUpdateModel.LastName = User.LastName;
                dbUpdateModel.Username = User.Username; 
                await _repo.EditToDoUserByIdAsync(dbUpdateModel);

                 ValidateUpdatePass(dbUpdateModel, User);

                 dbUpdateModel.Password = User.NewPassword;
                
             });

        private async Task<UserEntity> GetByUsernameAsync(string name)
           => await TryCatch(async () =>
           {
               var todoListItem = await _repo.GetToDoUserByUsernameAsync(name);
               return todoListItem;
           });

       
    }
}
