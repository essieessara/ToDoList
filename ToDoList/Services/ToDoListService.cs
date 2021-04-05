using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models;
using ToDoList.Repositories;

namespace ToDoList.Services
{
    public class ToDoListService : IToDoListService
    {

        private readonly IToDoListRepo _toDo;

        public ToDoListService(IToDoListRepo ToDo)
        {
            _toDo = ToDo;
        }

        public async Task<List<ToDoListEntity>> GetListAsync()
        {
            return await _toDo.GetAllToDoList();
        }

        public async Task<ToDoListEntity> GetById(int id)
        {
            return await _toDo.GetToDoById(id);
        }
        public async Task<ToDoListEntity> Create(CreateTodoItemModel toDodb)
        {
            ToDoListEntity dbModel = new ToDoListEntity()
            {
                CreatedDate = DateTime.Now,
                IsFinished = false,
                ItemName = toDodb.ItemName,
                EndedDate= null
            };

            return await _toDo.CreateToDoItem(dbModel);
        }



        public async Task DeleteAsync(int id)
        {
            var objectTovalidate = await _toDo.GetToDoById(id);
            if (objectTovalidate != null)
            {
                await _toDo.DeleteToDoById(id);
            }
            

        }


        public async Task Update(int id, UpdateTodoItemNameModel toDodb)
        {
            ToDoListEntity Model = await GetById(id);
       
            if (Model.IsFinished != false)
            {
                Console.WriteLine("Can not Update");

            }
            else
            {
                
                Model.ItemName = toDodb.ItemName;
               
                if (id == Model.ItemID)
                    try
                    {
                        await _toDo.EditToDoById(Model);
                    }
                    catch (Exception e)
                    {
                        if (id != Model.ItemID)
                            Console.WriteLine("IOException source: {0}", id);
                        throw;
                    }


            }


        }

        public async Task UpdateStatus(int id, UpdateTodoItemStatusModel toDodb)
        {
            ToDoListEntity Model = await GetById(id);
            if (Model.IsFinished != false)
            {
                Console.WriteLine("Can not Update");

            }
            else
            {
                Model.IsFinished = true;
                Model.EndedDate = DateTime.Now;

                if (id == Model.ItemID)
                    try
                    {
                        await _toDo.EditToDoById(Model);
                    }
                    catch (Exception e)
                    {
                        if (id != Model.ItemID)
                            Console.WriteLine("IOException source: {0}", id);
                        throw;
                    }

            }
        }

    }

}
