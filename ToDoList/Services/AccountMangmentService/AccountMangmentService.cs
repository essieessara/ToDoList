using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Services.ToDoServices;
using ToDoList.Services.UserServices;

namespace ToDoList.Services.AccountMangmentService
{
    public partial class AccountManagmentService : IAccountManagmentService
    {
        private readonly IUserService _userService;
        private readonly IToDoItemService _itemService;

        public AccountManagmentService(IUserService userService, IToDoItemService itemService)
        {
            _userService = userService;
            _itemService = itemService;
        }



        public Task DeleteUserAccountAsync(int id)
            => TryCatch(async () =>
            {

                var todoList = await _itemService.GetListAsync();

                foreach (var ToDo in todoList.ToList())
                {
                    await _itemService.DeleteAsync(ToDo.ItemID);
                }
                await _userService.DeleteUserAccountAsync(id);

            });

    }
}
