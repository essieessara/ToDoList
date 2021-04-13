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

        public ToDoListService(IToDoListRepo ToDo)
             =>_toDo = ToDo;
        

        public async Task<List<ToDoListEntity>> GetListAsync()
        {
            try
            {
                var todoList = await _toDo.GetAllToDoListASync();
                return ReturnValue(todoList);
            }
            catch (Exception)
            { 
                throw;
            }

        }
        public async Task<ToDoListEntity> GetByIdAsync(int id)
        {
            try
            {
                var todoListItem = await _toDo.GetToDoByIdAsync(id);
                return todoListItem;
            }
            catch (Exception)
            {
                throw;
            }


        }
        public async Task<ToDoListEntity> GetByName(string name)
        {
            try
            {
                var todoListItem = await _toDo.GetToDoByNameAsync(name);
                return ReturnValue(todoListItem);
            }
            catch (Exception)
            {
                throw;
            }


        }
        public async Task<ToDoListEntity> CreateAsync( CreateTodoItemModel toDodb)
        {
            
            try
            {
                var dbExistingModel = await GetByName(toDodb.ItemName);
                ToDoListEntity dbCreateModel = new ToDoListEntity()
                {
                    CreatedDate = DateTime.Now,
                    IsFinished = false,
                    ItemName = toDodb.ItemName,
                    EndedDate = null
                };

                if (toDodb != null)
                {
                        var todoNewItem = await _toDo.CreateToDoItemAsync(dbCreateModel);
                        return todoNewItem;
 
                }

                throw new ToDoAlreadyExistsException();



            }

            catch (Exception)
            {
                throw;
            }





        }
        public async Task DeleteAsync(int id)
        {
            try
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
            }

            catch (Exception)
            {
                throw;
            }

        }
        public async Task UpdateAsync(UpdateTodoItemNameModel toDodb)
        {   
            try
            {
                ToDoListEntity Model = await GetByIdAsync(toDodb.ItemID);
                if (Model.IsFinished != false)
                {
                    Console.WriteLine("Can not Update");

                }
                else
                {
                    Model.ItemName = toDodb.ItemName;
                    await _toDo.EditToDoByIdAsync(Model);
                }
            }
            catch (Exception)
            {
                throw new ToDoNotFoundException();
            }



        }
        public async Task UpdateStatusAsync(int id)
        {
            ToDoListEntity Model = await GetByIdAsync(id);
            try
            {
                if (Model.IsFinished != false)
                {
                    Console.WriteLine("Can not Update");

                }
                else
                {
                    Model.IsFinished = true;
                    Model.EndedDate = DateTime.Now;
                    await _toDo.EditToDoByIdAsync(Model);
                }
            }
            catch (Exception)
            {
                throw new ToDoNotFoundException();
            }

        }

        private T ReturnValue<T>(T model) 
        { 
                return model; 
        }

    }

}
