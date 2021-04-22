using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.ToDoItemsModels;

namespace ToDoList.Mapper
{
    public class ToDoItemMapper
    {

        public ToDoItemtEntity Map(CreateTodoItemModel model, UserEntity user)
        {
            return new ToDoItemtEntity
            {
                CreatedDate = DateTime.Now,
                IsFinished = false,
                ItemName = model.ItemName,
                EndedDate = null,
                User = user,
                UserID = model.UserID
            };
        }
        public ToDoItemResponseModel Map(ToDoItemtEntity model)
        {
            return new ToDoItemResponseModel
            {
                ItemID = model.ItemID,
                ItemName = model.ItemName,
                CreatedDate = model.CreatedDate,
                EndedDate = model.EndedDate,
                IsFinished = model.IsFinished
            };
        }
        public ToDoItemResponseModel Map(ToDoItemtEntity model, UserEntity user)
        {
            var output = new ToDoItemResponseModel
            {
                ItemID = model.ItemID,
                ItemName = model.ItemName,
                CreatedDate = model.CreatedDate,
                EndedDate = model.EndedDate,
                IsFinished = model.IsFinished
            };
            output.UserData = new UserDataResponseModel
            {
                UserID = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username
            };
            return output;
        }
    }
}
