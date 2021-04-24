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
using ToDoList.Services.UserServices;

namespace ToDoList.Services.ToDoServices
{
    public partial class ToDoItemService : IToDoItemService
    {

        private readonly IToDoItemRepo _repo;
        private readonly IUserService _user;
        private readonly ToDoItemMapper _mapper;
        public ToDoItemService(IToDoItemRepo ToDo, IUserService User)
        {
            _repo = ToDo;
            _user = User;
            _mapper = new();
        }

        public Task<List<ToDoItemtEntity>> GetListAsync()
             => TryCatch(async () =>
             {
                 var todoList = await _repo.GetAllToDoListASync();
                 return todoList;
             });
        public Task<ToDoItemtEntity> GetByIdAsync(int id)
            => TryCatch(async () =>
           {
               var todoListItem = await _repo.GetToDoByIdAsync(id);

               ValidateGetByID(todoListItem);
               return todoListItem;

           });
        public Task<ToDoItemResponseModel> CreateAsync(CreateTodoItemModel toDodb)
             => TryCatch(async () =>
             {
                 // declare ur variables
                 // validate your data
                 // do whats nessasary

                 UserDataResponseModel user = await _user.GetUserByIdAsync(toDodb.UserID);
                 ToDoItemtEntity dbCreateModel = _mapper.Map(toDodb);
                 await ValidateCreateToDoItem(toDodb, user);

                 await _repo.CreateToDoItemAsync(dbCreateModel);
     
               //  dbCreateModel.User = await _user.GetUserByIdAsync(user.UserID);
                 var output = _mapper.Map(dbCreateModel);
                 output.UserData = user;
                 return output;

               

             });
        public Task DeleteAsync(int id)
             => TryCatch(async () =>
            {
                ToDoItemtEntity objectTovalidate = await _repo.GetToDoByIdAsync(id);
                ValidateDelete(objectTovalidate);
                await _repo.DeleteToDoByIdAsync(id);
            });
        public Task UpdateToDoNameAsync(UpdateTodoItemNameModel toDo)
             => TryCatch(async () =>
            {
                ToDoItemtEntity dbUpdateModel = await GetByIdAsync(toDo.ItemID);

                ValidateUpdateName(dbUpdateModel);

                dbUpdateModel.ItemName = toDo.ItemName;
                await _repo.EditToDoByIdAsync(dbUpdateModel);
                   

            });
        public Task UpdateStatusAsync(int id)
             => TryCatch(async () =>
            {

                ToDoItemtEntity Model = await GetByIdAsync(id);

                ValidateUpdateStatus(Model);

                 Model.IsFinished = true;
                 Model.EndedDate = DateTime.Now;
                 await _repo.EditToDoByIdAsync(Model);
                   
            });

    }

   
}
