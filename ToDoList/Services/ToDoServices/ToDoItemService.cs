using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions.ToDoItemExceptions;
using ToDoList.Exceptions.UserExceptions;
using ToDoList.Mapper;
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
        private readonly ToDoItemMapper _mapper;
        public ToDoItemService(IToDoItemRepo ToDo, IUserRepo User)
        {
            _toDo = ToDo;
            _user = User;
            _mapper = new();
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
                 // declare ur virables
                 // validate your data
                 // do whats nessasary

                 UserEntity user = await _user.GetToDoUserByIdAsync(toDodb.UserID);
                 ToDoItemtEntity dbCreateModel = _mapper.Map(toDodb, user);
                 await ValidateCreateToDoItem(toDodb, user);

                 await _toDo.CreateToDoItemAsync(dbCreateModel);
     
                 dbCreateModel.User = await _user.GetToDoUserByIdAsync(user.UserID);
                 var output = _mapper.Map(dbCreateModel, user);
                 return output;

               

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


        private async Task ValidateCreateToDoItem(CreateTodoItemModel model, UserEntity user)
        {
            if (user is null) { throw new UserNotFoundException(); }
            if (model is null) { throw new ToDoValueIsNullException(); }

            user.Lists = await GetUserByIdAsync(model.UserID);
            var existToDoOFUser = user.Lists
                .Where(x => x.ItemName == model.ItemName).ToList();
            if (existToDoOFUser.Count > 0)
            {
                throw new ToDoAlreadyExistsException();
            }
        }

    }

}
