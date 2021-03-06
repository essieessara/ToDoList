using System;
using ToDoList.Database;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.ToDoItemsModels;

namespace ToDoList.Helpers.Mapper
{
    public class ToDoItemMapper
    {

        public ToDoItemtEntity Map(CreateTodoItemModel model)
        {
            return new ToDoItemtEntity
            {
                ItemID = model.ItemID,
                CreatedDate = DateTime.Now,
                IsFinished = false,
                ItemName = model.ItemName,
                EndedDate = null,
                // UserID = model.UserID
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

    }
}
