using LanguageExt;
using LanguageExt.ClassInstances.Pred;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
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
        {
            _toDo = ToDo;
        }

        public async Task<List<ToDoListEntity>> GetListAsync()
        {
            var todoList = await _toDo.GetAllToDoList();
            try
            {
                if (todoList != null)
                {
  
                        return todoList;
                }
   
                return null;

            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<ToDoListEntity> GetById(int id)
        {
            var todoListItem = await _toDo.GetToDoById(id);
            try
            {
                if (todoListItem != null)
                {

                        return todoListItem;
        
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
           

        }
        public async Task<ToDoListEntity> GetByName(string name)
        {
            var todoListItem = await _toDo.GetToDoByName(name);
            try
            {
                if (todoListItem != null)
                {

                    return todoListItem;

                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }


        }
        public async Task<ToDoListEntity> Create(string name, CreateTodoItemModel toDodb)
        {
            var dbItemname = await GetByName(name);
             ToDoListEntity dbModel = new ToDoListEntity()
            {
                CreatedDate = DateTime.Now,
                IsFinished = false,
                ItemName = toDodb.ItemName,
                EndedDate= null
            };
           
            try
            {
                if (dbItemname == null)
                {
                    var todoNewItem = await _toDo.CreateToDoItem(dbModel);
                    return todoNewItem;
                }
                else
                {

                        if (dbItemname.ItemName != dbModel.ItemName)
                        {
                            if (toDodb != null)
                            {

                                var todoNewItem = await _toDo.CreateToDoItem(dbModel);
                                return todoNewItem;

                            }


                        }
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
            var objectTovalidate = await _toDo.GetToDoById(id);
            try
            {
                if (objectTovalidate != null)
                {
                    if (objectTovalidate.ItemID == id) { 
                    
                        await _toDo.DeleteToDoById(id);
                    }
                    else
                    {
                        throw new ToDoNotFoundException();
                    }
                 

                }
                else
                {
                    throw new ToDoNotFoundException();
                }


            }
        
            catch (Exception)
            {
                throw ;
            }

        }
        public async Task Update(int id, UpdateTodoItemNameModel toDodb)
        {
            ToDoListEntity Model = await GetById(id);
            try
            {
                if (Model.IsFinished != false)
                {
                    Console.WriteLine("Can not Update");

                }
                else
                {

                    Model.ItemName = toDodb.ItemName;

                    if (id == Model.ItemID)
                    {
                        await _toDo.EditToDoById(Model);
                    }
                    else
                    {
                        throw new ToDoNotFoundException();
                    }



                }
            }
            catch (Exception)
            {
                throw;
            }
            


        }
        public async Task UpdateStatus(int id, UpdateTodoItemStatusModel toDodb)
        {
            ToDoListEntity Model = await GetById(id);
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

                    if (id == Model.ItemID)
                    {
                        await _toDo.EditToDoById(Model);
                    }

                    else
                    {
                        throw new ToDoNotFoundException();
                    }



                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

    }

}
