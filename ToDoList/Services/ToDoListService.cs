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


        public async Task Update(int id, UpdateTodoItemModel toDodb)
        {
            ToDoListEntity dbModel = new ToDoListEntity()
            {
                ItemID = id ,
                CreatedDate = DateTime.Now,
                IsFinished = false,
                ItemName = toDodb.ItemName,
                EndedDate = null
            };

            if (id == dbModel.ItemID)
                try
                {
                    await _toDo.EditToDoById(dbModel); 
                }
                catch (Exception e)
                {
                    if (id != dbModel.ItemID)
                        Console.WriteLine("IOException source: {0}", id);
                    throw;
                }   


        }

        public async Task Update(int id, ToDoListEntity toDodb)
        {
            toDodb.ItemID = id;
            toDodb.IsFinished = true;
            toDodb.EndedDate = DateTime.Now;

            if (id == toDodb.ItemID)
                try
                {
                    await _toDo.EditToDoById(toDodb);
                }
                catch (Exception e)
                {
                    if (id != toDodb.ItemID)
                        Console.WriteLine("IOException source: {0}", id);
                    throw;
                }


        }

    }

}
