using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions.ToDoItemExceptions;
using ToDoList.Exceptions.UserExceptions;
using ToDoList.Helpers.Mapper;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.ToDoItemsModels;
using ToDoList.Services.AccountMangmentService;
using ToDoList.Services.ToDoServices;

namespace ToDoList.Services.DataManagementService
{
    public partial class DataManagementService : IDataManagementService
    {
        private readonly IAccountManagmentService _account;
        private readonly IToDoItemService _itemService;
        private readonly ToDoItemMapper _mapper;
        private readonly ClaimsPrincipal _loggedUser;

        public DataManagementService(IAccountManagmentService account, IToDoItemService itemService,
            IHttpContextAccessor contextAccessor)
        {
            _account = account;
            _itemService = itemService;
            _mapper = new();
            _loggedUser = contextAccessor.HttpContext.User;
        }
        public Task<ToDoItemResponseModel> CreateAsync(CreateTodoItemModel toDodb)
            => TryCatch(async () =>
            {
                Validateauthentication();
                int userId = Convert.ToInt32(Login().Value);
                var user = await _account.GetUserByIdAsync(userId);
                ToDoItemtEntity dbCreateModel = _mapper.Map(toDodb);
                await ValidateCreateToDoItem(toDodb, user);

                return await _itemService.CreateAsync(toDodb);

            });

        private async Task ValidateCreateToDoItem(CreateTodoItemModel model, UserDataResponseModel user)
        {
            if (user is null) { throw new UserNotFoundException(); }
            if (model is null) { throw new ToDoValueIsNullException(); }

            var toDoList = await _itemService.GetUserToDoListByIdAsync();
            if (toDoList is not null)
            {

                int countExisits = toDoList.Count(x => x.ItemName == model.ItemName);
                if (countExisits > 0)
                {
                    throw new ToDoAlreadyExistsException();
                }
            }

        }
        private Claim Login()
        {
            Validateauthentication();
            Claim userClaim = _loggedUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
            ValidateLogin(userClaim);
            return userClaim;
        }

        private void Validateauthentication()
        {
            if (!_loggedUser.Identity.IsAuthenticated) { throw new UserNotLoggedInException(); }
        }
        private void ValidateLogin(Claim claim)
        {
            if (claim is null) { throw new UserNotLoggedInException(); }
        }
    }
}
