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
using ToDoList.Services.AccountMangmentService;
using ToDoList.Services.ToDoServices;
using ToDoList.Services.UserServices;

namespace ToDoList.Services.DataManagementService
{
    public partial class DataManagementService : IDataManagementService
    {
        private readonly IAccountManagmentService _account;
        private readonly IToDoItemService _itemService;
        private readonly ToDoItemMapper _mapper;


        public DataManagementService(IAccountManagmentService account, IToDoItemService itemService)
        {
            _account = account;
            _itemService = itemService;
            _mapper = new();
        }
        public Task<ToDoItemResponseModel> CreateAsync(CreateTodoItemModel toDodb)
            => TryCatch(async () =>
            {
                var user = await _account.GetUserByIdAsync(toDodb.UserID);
                ToDoItemtEntity dbCreateModel = _mapper.Map(toDodb);
                await ValidateCreateToDoItem(toDodb, user);

                return await _itemService.CreateAsync(toDodb);

            });

        private async Task ValidateCreateToDoItem(CreateTodoItemModel model, UserDataResponseModel user)
        {
            if (user is null) { throw new UserNotFoundException(); }
            if (model is null) { throw new ToDoValueIsNullException(); }

            var toDoList = await _itemService.GetListOfUserByIdAsync(model.UserID);
            if (toDoList is not null)
            {

                int countExisits = toDoList.Count(x => x.ItemName == model.ItemName);
                if (countExisits > 0)
                {
                    throw new ToDoAlreadyExistsException();
                }
            }

        }
    }
}
