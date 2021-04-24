using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions.ToDoItemExceptions;
using ToDoList.Exceptions.UserExceptions;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.ToDoItemsModels;

namespace ToDoList.Services.ToDoServices
{
    public partial class ToDoItemService
    {
        private void ValidateGetByID(ToDoItemtEntity model)
        {
            if (model is null) { throw new ToDoNotFoundException(); }
        }
        private async Task ValidateCreateToDoItem(CreateTodoItemModel model, UserDataResponseModel user)
        {
            if (user is null) { throw new UserNotFoundException(); }
            if (model is null) { throw new ToDoValueIsNullException(); }

            var userList = (ICollection<ToDoItemtEntity>) await _user.GetUserByIdAsync(model.UserID);
            int countExisits = userList.Count(x => x.ItemName == model.ItemName);
            if (countExisits > 0)
            {
                throw new ToDoAlreadyExistsException();
            }
        }

        private void ValidateDelete(ToDoItemtEntity model)
        {
            if (model is null) { throw new ToDoNotFoundException(); }
        }

        private void ValidateUpdateName(ToDoItemtEntity model)
        {
            if (model is null) { throw new ToDoNotFoundException(); }
            if (model.IsFinished == true) { throw new CanNotUpdateToDoException(); }
        }

        private void ValidateUpdateStatus(ToDoItemtEntity model)
        {
            if (model is null) { throw new ToDoNotFoundException(); }
            if (model.IsFinished == true) { throw new CanNotUpdateToDoException(); }
        }
    }
}
