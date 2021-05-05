﻿using System;
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
using ToDoList.Services.AccountMangmentService;
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
        public Task<ToDoItemtEntity> GetByIdAsync(int id , int uid)
            => TryCatch(async () =>
           {
               var todoListItem = await _repo.GetToDoByIdAsync(id , uid);

               ValidateGetByID(todoListItem);
               return todoListItem;

           });
        public Task<List<ToDoItemtEntity>> GetUserToDoListByIdAsync(int id)
         => TryCatch(async () =>
         {
             var todoListUser = await _repo.GetToDoUserByIdAsync(id);

             ValidateGetListOfUserByID(todoListUser);

             return todoListUser;

         });
        public Task<ToDoItemResponseModel> CreateAsync(CreateTodoItemModel toDodb)
             => TryCatch(async () =>
             {

                 ToDoItemtEntity dbCreateModel = _mapper.Map(toDodb);
                 var result = await _repo.CreateToDoItemAsync(dbCreateModel);
                 var output = _mapper.Map(result);
                 return output;

             });
        public Task DeleteAsync(int id , int uid)
             => TryCatch(async () =>
            {
                ToDoItemtEntity objectTovalidate = await _repo.GetToDoByIdAsync(id , uid);
                ValidateDelete(objectTovalidate);
                await _repo.DeleteToDoByIdAsync(id , uid);
            });
        public Task<ToDoItemtEntity> UpdateToDoNameAsync(UpdateTodoItemNameModel toDo)
             => TryCatch(async () =>
             {
                 ToDoItemtEntity dbUpdateModel = await GetByIdAsync(toDo.ItemID , toDo.UserID);
                 await ValidateUpdateNameAsync(dbUpdateModel, toDo);

                 dbUpdateModel.ItemName = toDo.ItemName;
                 var output = await _repo.EditToDoByIdAsync(dbUpdateModel);
                 return output;

             });
        public Task <ToDoItemtEntity> UpdateStatusAsync(int id , int uid)
             => TryCatch(async () =>
             {

                ToDoItemtEntity Model = await GetByIdAsync(id , uid);

                ValidateUpdateStatus(Model);

                Model.IsFinished = true;
                Model.EndedDate = DateTime.Now;
                var output = await _repo.EditToDoByIdAsync(Model);
                return output;
             });

    }


}
