using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions;
using ToDoList.Models;
using ToDoList.Repositories;


namespace ToDoList.Services
{
    public partial class ToDoListService : IToDoListService
    {

        private readonly IToDoListRepo _toDo;
       
        public ToDoListService(IToDoListRepo ToDo)
             =>_toDo = ToDo;


        public  Task<List<ToDoListEntity>> GetListAsync()
             =>  TryCatch(async () =>
              {
                 var todoList = await _toDo.GetAllToDoListASync();
                 return todoList;
             });
        public  Task<ToDoListEntity> GetByIdAsync(int id)
            =>  TryCatch(async () =>
            {
                var todoListItem = await _toDo.GetToDoByIdAsync(id);
                if(todoListItem != null)
                {
                    return todoListItem;
                }
                else
                {
                    throw new ToDoNotFoundException();
                }
            });      
        public  Task<ToDoListEntity> CreateAsync(CreateTodoItemModel toDodb)
             =>  TryCatch(async () =>
             {
                 var dbExistingModel = await GetByNameAsync(toDodb.ItemName);
                 ToDoListEntity dbCreateModel = new ToDoListEntity()
                 {
                     CreatedDate = DateTime.Now,
                     IsFinished = false,
                     ItemName = toDodb.ItemName,
                     EndedDate = null
                 };
                 if (toDodb != null)
                 {
                     if (dbExistingModel == null)
                     {
                         var todoNewItem = await _toDo.CreateToDoItemAsync(dbCreateModel);
                         return todoNewItem;
                     }
                     throw new ToDoAlreadyExistsException();

                 }
                 throw new ToDoValueIsNullException();

             });
        public  Task DeleteAsync(int id)
             =>  TryCatch(async () =>
             {
                     var objectTovalidate = await _toDo.GetToDoByIdAsync(id);
                     if (objectTovalidate != null)
                     {
                         await _toDo.DeleteToDoByIdAsync(id);
                     }
                     else
                     {
                         throw new ToDoNotFoundException();
                     }
             });
        public  Task UpdateToDoNameAsync(UpdateTodoItemNameModel toDo)
             =>  TryCatch(async () =>
             {
                 ToDoListEntity dbUpdateModel = await GetByIdAsync(toDo.ItemID);


                 if (dbUpdateModel != null)
                 {
                     if (dbUpdateModel.IsFinished == false)
                     {
                         dbUpdateModel.ItemName = toDo.ItemName;
                         await _toDo.EditToDoByIdAsync(dbUpdateModel);
                     }
                     else
                     {
                         throw new CanNotUpdateException();
                     }
                 }
                 else
                     throw new ToDoNotFoundException();

             });
        public  Task UpdateStatusAsync(int id)
             =>  TryCatch(async () =>
             {
                 
                 ToDoListEntity Model = await GetByIdAsync(id);

                 if (Model != null)
                 {
                     if (Model.IsFinished == false)
                     {
                         Model.IsFinished = true;
                         Model.EndedDate = DateTime.Now;
                         await _toDo.EditToDoByIdAsync(Model);
                     }
                     else
                     {
                         throw new CanNotUpdateException();
                     }
                 }
                 else
                     throw new ToDoNotFoundException();
             });


        private async Task<ToDoListEntity> GetByNameAsync(string name)
             => await TryCatch(async () =>
             {
                 var todoListItem = await _toDo.GetToDoByNameAsync(name);
                 return todoListItem;
             });

    }

}
