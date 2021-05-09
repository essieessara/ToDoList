using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions.ToDoItemExceptions;
using ToDoList.Exceptions.UserExceptions;
using ToDoList.Models.ToDoItemsModels;

namespace ToDoList.Services.ToDoServices
{
    public partial class ToDoItemService
    {
        private void ValidateGetByID(ToDoItemtEntity model)
        {
            if (model is null) { throw new ToDoNotFoundException(); }
        }

        private void ValidateDelete(ToDoItemtEntity model)
        {
            if (model is null) { throw new ToDoNotFoundException(); }
        }

        private async Task ValidateUpdateNameAsync(ToDoItemtEntity model, UpdateTodoItemNameModel modelToBeUpdated)
        {
            if (model is null) { throw new ToDoNotFoundException(); }
            if (model.IsFinished == true) { throw new CanNotUpdateToDoException(); }
            var toDoList = await GetUserToDoListByIdAsync();
            if (toDoList is not null)
            {

                int countExisits = toDoList.Count(x => x.ItemName == modelToBeUpdated.ItemName);
                if (countExisits > 0)
                {
                    throw new ToDoAlreadyExistsException();
                }
            }
        }

        private void ValidateUpdateStatus(ToDoItemtEntity model)
        {
            if (model is null) { throw new ToDoNotFoundException(); }
            if (model.IsFinished == true) { throw new CanNotUpdateToDoException(); }
        }

        private void ValidateGetListOfUserByID(List<ToDoItemtEntity> model)
        {
            if (model is null) { throw new ToDoNotFoundException(); }

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
