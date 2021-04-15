using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions;
using ToDoList.Models;
using ToDoList.Repositories;

namespace ToDoList.Services
{
    public partial class ToDoUserService : IToDoUserService
    {
        private readonly IToDoUsersRepo _user;

        public ToDoUserService(IToDoUsersRepo User)
          => _user = User;

        public Task<List<ToDoUsersEntity>> GetUserListAsync()
             => TryCatch(async () =>
             {
                 var todoUserList = await _user.GetAllToDoUsersListASync();
                 return todoUserList;
             });
        public Task<ToDoUsersEntity> GetUserByIdAsync(int id)
            => TryCatch(async () =>
            {
                var todoListUser = await _user.GetToDoUserByIdAsync(id);
                if (todoListUser != null)
                {
                    return todoListUser;
                }
                else
                {
                    throw new ToDoUserNotFoundException();
                }
            });
        public Task DeleteUserAccountAsync(int id)
            => TryCatch(async () =>
            {
                var objectTovalidate = await _user.GetToDoUserByIdAsync(id);
                if (objectTovalidate != null)
                {
                    await _user.DeleteToDoUserByIdAsync(id);
                }
                else
                {
                    throw new ToDoUserNotFoundException();
                }
            });

        public Task<ToDoUsersEntity> RegisterUserAsync(RegisterToDoUser toDoUser)
             => TryCatch(async () =>
             {
                 var dbExistingModel = await GetByUsernameAsync(toDoUser.Username);
                 ToDoUsersEntity dbCreateUser = new ToDoUsersEntity()
                 {
                     FirstName = toDoUser.FirstName,
                     LastName = toDoUser.LastName,
                     Username = toDoUser.Username,
                     Password = toDoUser.Password

                 };
                 if (toDoUser != null)
                 {
                     if (dbExistingModel == null)
                     {
                         var todoNewUser = await _user.CreateToDoUserAsync(dbCreateUser);
                         return todoNewUser;
                     }
                     throw new ToDoUserAlreadyExistsException();

                 }
                 throw new ToDoUserValueIsNullException();

             });

        public Task UpdateToDoUserAsync(UpdateTodoUserModel User)
             => TryCatch(async () =>
             {
                 ToDoUsersEntity dbUpdateModel = await GetUserByIdAsync(User.UserID);


                 if (dbUpdateModel != null)
                 {
                     dbUpdateModel.UserID = User.UserID;
                     dbUpdateModel.FirstName = User.FirstName;
                     dbUpdateModel.LastName = User.LastName;
                     dbUpdateModel.Username = User.Username;
                     dbUpdateModel.Password = User.Password;
                     await _user.EditToDoUserByIdAsync(dbUpdateModel);
                 }
                 else
                     throw new ToDoUserNotFoundException();

             });

        private async Task<ToDoUsersEntity> GetByUsernameAsync(string name)
           => await TryCatch(async () =>
           {
               var todoListItem = await _user.GetToDoUserByUsernameAsync(name);
               return todoListItem;
           });

    }
}
