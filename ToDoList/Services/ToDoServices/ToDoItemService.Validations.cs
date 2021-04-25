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

        private void ValidateGetListOfUserByID(List<ToDoItemtEntity> model)
        {
            if (model is null) { throw new ToDoNotFoundException(); }
        }
    }
}
