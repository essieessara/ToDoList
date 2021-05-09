using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Helpers.Mapper;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.ToDoItemsModels;
using ToDoList.Repositories.ToDoItemRepos;
using ToDoList.Services.UserServices;

namespace ToDoList.Services.ToDoServices
{
    public partial class ToDoItemService : IToDoItemService
    {

        private readonly IToDoItemRepo _repo;
        private readonly ToDoItemMapper _mapper;
        private readonly ClaimsPrincipal _loggedUser;

        public ToDoItemService(IToDoItemRepo ToDo,
            IUserService User, IHttpContextAccessor contextAccessor)
        {
            _repo = ToDo;
            _mapper = new();
            _loggedUser = contextAccessor.HttpContext.User;

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
                var todoListItem = await _repo.GetToDoByIdAsync(id, loginUser());

                ValidateGetByID(todoListItem);
                return todoListItem;

            });
        public Task<List<ToDoItemtEntity>> GetUserToDoListByIdAsync()
         => TryCatch(async () =>
         {

             var todoListUser = await _repo.GetToDoUserByIdAsync(loginUser());

             ValidateGetListOfUserByID(todoListUser);

             return todoListUser;

         });
        public Task<ToDoItemResponseModel> CreateAsync(CreateTodoItemModel toDodb)
             => TryCatch(async () =>
             {

                 ToDoItemtEntity dbCreateModel = _mapper.Map(toDodb);
                 dbCreateModel.UserID = loginUser();
                 var result = await _repo.CreateToDoItemAsync(dbCreateModel);
                 var output = _mapper.Map(result);
                 return output;

             });
        public Task DeleteAsync(int id)
             => TryCatch(async () =>
             {
                 ToDoItemtEntity objectTovalidate = await _repo.GetToDoByIdAsync(id, loginUser());
                 ValidateDelete(objectTovalidate);
                 await _repo.DeleteToDoByIdAsync(id);
             });
        public Task<ToDoItemtEntity> UpdateToDoNameAsync(UpdateTodoItemNameModel toDo)
             => TryCatch(async () =>
             {
                 ToDoItemtEntity dbUpdateModel = await GetByIdAsync(toDo.ItemID);
                 await ValidateUpdateNameAsync(dbUpdateModel, toDo);

                 dbUpdateModel.ItemName = toDo.ItemName;
                 var output = await _repo.EditToDoByIdAsync(dbUpdateModel);
                 return output;

             });
        public Task<ToDoItemtEntity> UpdateStatusAsync(int id)
             => TryCatch(async () =>
             {

                 ToDoItemtEntity Model = await GetByIdAsync(id);

                 ValidateUpdateStatus(Model);

                 Model.IsFinished = true;
                 Model.EndedDate = DateTime.Now;
                 var output = await _repo.EditToDoByIdAsync(Model);
                 return output;
             });

        private Claim Login()
        {
            Validateauthentication();
            Claim userClaim = _loggedUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
            ValidateLogin(userClaim);
            return userClaim;
        }
        private int loginUser()
        {
            int userId = Convert.ToInt32(Login().Value);
            return userId;
        }

    }


}
