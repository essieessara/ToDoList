using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions;
using ToDoList.Models;
using ToDoList.Repositories;


namespace ToDoList.Services
{
    public class ToDoListService : IToDoListService
    {

        private readonly IToDoListRepo _toDo;
        private delegate Task Function();
        private delegate Task<T> Function<T>();
        public ToDoListService(IToDoListRepo ToDo)
             =>_toDo = ToDo;


        public async Task<List<ToDoListEntity>> GetListAsync()
             => await TryCatch(async () =>
              {
                 var todoList = await _toDo.GetAllToDoListASync();
                 return todoList;
             });
        public async Task<ToDoListEntity> GetByIdAsync(int id)
            => await TryCatch(async () =>
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
        public async Task<ToDoListEntity> GetByNameAsync(string name)
             => await TryCatch(async () =>
             {
                 var todoListItem = await _toDo.GetToDoByNameAsync(name);
                 return todoListItem;
             });
        public async Task<ToDoListEntity> CreateAsync(CreateTodoItemModel toDodb)
             => await TryCatch(async () =>
             {
                 var dbExistingModel = await GetByNameAsync(toDodb.ItemName);
                 ToDoListEntity dbCreateModel = new ToDoListEntity()
                 {
                     CreatedDate = DateTime.Now,
                     IsFinished = false,
                     ItemName = toDodb.ItemName,
                     EndedDate = null
                 };
                 if (dbExistingModel != null) 
                 {
                     if (dbExistingModel.ItemName != dbCreateModel.ItemName)
                     {
                         var todoNewItem = await _toDo.CreateToDoItemAsync(dbCreateModel);
                         return todoNewItem;

                         
                     }
                     throw new ToDoAlreadyExistsException();
                 }
                throw new ToDoValueIsNullException();

             });
        public async Task DeleteAsync(int id)
             => await TryCatch(async () =>
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
        public async Task UpdateToDoNameAsync(int id, UpdateTodoItemNameModel toDo)
             => await TryCatch(async () =>
             {
                 ToDoListEntity dbUpdateModel = await GetByIdAsync(id);


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
        public async Task UpdateStatusAsync(int id)
             => await TryCatch(async () =>
             {
                 
                 ToDoListEntity Model = await GetByIdAsync(id);

                 if (Model != null)
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

                 throw new ToDoNotFoundException();


             });

        private async Task TryCatch(Function model)
        {
            try
            {
                await model();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<T> TryCatch<T>(Function<T> model)
        {
            try
            {
                return await model();
            }
            catch (Exception)
            {
                throw;

            }
        }
    }

}
