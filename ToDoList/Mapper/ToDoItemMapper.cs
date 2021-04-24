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

        public ToDoItemtEntity Map(CreateTodoItemModel model)
        {
            return new ToDoItemtEntity
            {
                CreatedDate = DateTime.Now,
                IsFinished = false,
                ItemName = model.ItemName,
                EndedDate = null,
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
      
    }
}
