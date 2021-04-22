using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions.ToDoItemExceptions;
using ToDoList.Exceptions.UserExceptions;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.ToDoItemsModels;
using ToDoList.Repositories.ToDoItemRepos;
using ToDoList.Repositories.UserRepos;

namespace ToDoList.Services.ToDoServices
{
    public partial class ToDoItemService : IToDoItemService
    {

        private readonly IToDoItemRepo _toDo;
        private readonly IUserRepo _user;
        public ToDoItemService(IToDoItemRepo ToDo, IUserRepo User)
        {
            _toDo = ToDo;
            _user = User;
        }

        public Task<List<ToDoItemtEntity>> GetListAsync()
             => TryCatch(async () =>
             {
                 var todoList = await _toDo.GetAllToDoListASync();
                 return todoList;
             });
        public Task<ToDoItemtEntity> GetByIdAsync(int id)
            => TryCatch(async () =>
           {
               var todoListItem = await _toDo.GetToDoByIdAsync(id);
               if (todoListItem != null)
               {
                   return todoListItem;
               }
               else
               {
                   throw new ToDoNotFoundException();
               }
           });
        public Task<List<ToDoItemtEntity>> GetUserByIdAsync(int id)
            => TryCatch(async () =>
           {
               var todoListUser = await _toDo.GetToDoUserByIdAsync(id);
               if (todoListUser != null)
               {
                   return todoListUser;
               }
               else
               {
                   throw new ToDoNotFoundException();
               }
           });
        public Task<ToDoItemResponseModel> CreateAsync(CreateTodoItemModel toDodb)
             => TryCatch(async () =>
             {

                var user = await _user.GetToDoUserByIdAsync(toDodb.UserID);

                ToDoItemtEntity dbCreateModel = new ToDoItemtEntity()
                {
                    CreatedDate = DateTime.Now,
                    IsFinished = false,
                    ItemName = toDodb.ItemName,
                    EndedDate = null,
                    User = user,
                    UserID = toDodb.UserID 
                };
               if (user != null)
               {
                 if (toDodb != null)
                 {
                    user.Lists = await GetUserByIdAsync(toDodb.UserID);
                    var existToDoOFUser = user.Lists
                        .Where(x => x.UserID == toDodb.UserID && x.ItemName == toDodb.ItemName).ToList();

                    if ( existToDoOFUser.Count == 0)
                    {
                                await _toDo.CreateToDoItemAsync(dbCreateModel);

                                var output = new ToDoItemResponseModel()
                                {
                                    ItemID = dbCreateModel.ItemID,
                                    ItemName = dbCreateModel.ItemName,
                                    CreatedDate = dbCreateModel.CreatedDate,
                                    EndedDate = dbCreateModel.EndedDate,
                                    IsFinished = dbCreateModel.IsFinished
                                };

                                dbCreateModel.User = await _user.GetToDoUserByIdAsync(user.UserID);
                                output.UserData = new UserDataResponseModel()
                                {
                                    UserID = user.UserID,
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    Username = user.Username
                                };
                                return output;
                    }

                    throw new ToDoAlreadyExistsException();
   
                 }
                    throw new ToDoValueIsNullException();
               }
                throw new UserNotFoundException();

            });
        public Task DeleteAsync(int id)
             => TryCatch(async () =>
            {
                var objectTovalidate = await _toDo.GetToDoByIdAsync(id);
                if (objectTovalidate != null)
                {
                    await _toDo.DeleteToDoByIdAsync(id);
                }
                else
                {
                    throw new ToDoNotFoundException();
                }
            });
        public Task UpdateToDoNameAsync(UpdateTodoItemNameModel toDo)
             => TryCatch(async () =>
            {
                ToDoItemtEntity dbUpdateModel = await GetByIdAsync(toDo.ItemID);


                if (dbUpdateModel != null)
                {
                    if (dbUpdateModel.IsFinished == false)
                    {
                        dbUpdateModel.ItemName = toDo.ItemName;
                        await _toDo.EditToDoByIdAsync(dbUpdateModel);
                    }
                    else
                    {
                        throw new CanNotUpdateToDoException();
                    }
                }
                else
                    throw new ToDoNotFoundException();

            });
        public Task UpdateStatusAsync(int id)
             => TryCatch(async () =>
            {

                ToDoItemtEntity Model = await GetByIdAsync(id);

                if (Model != null)
                {
                    if (Model.IsFinished == false)
                    {
                        Model.IsFinished = true;
                        Model.EndedDate = DateTime.Now;
                        await _toDo.EditToDoByIdAsync(Model);
                    }
                    else
                    {
                        throw new CanNotUpdateToDoException();
                    }
                }
                else
                    throw new ToDoNotFoundException();
            });

    }

}
