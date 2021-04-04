using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Repositories;

namespace ToDoList.Services
{
    public class ToDoListService 
    {
        // interface
        private readonly ToDoListRepo _toDo;

        public ToDoListService(ToDoListRepo ToDo)
        {
            _toDo = ToDo;
        }

       public async Task<List<ToDoListEntity>> GetListAsync()
        {
            return await _toDo.GetAllToDoList();
        }

        public async Task<List<ToDoListEntity>> GetById(int id)
        {
            //return await _toDo.GetToDoById(id);
        }

        public async Task<List<ToDoListEntity>> Update(int id, ToDoListEntity toDodb)
        {
            //return await _toDo.EditToDoById(id,toDodb);
        }

    }

}
