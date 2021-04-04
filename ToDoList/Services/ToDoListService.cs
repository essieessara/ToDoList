using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Repositories;

namespace ToDoList.Services
{
    public class ToDoListService : IToDoListService
    {

        private readonly ToDoListRepo _toDo;

        public ToDoListService(ToDoListRepo ToDo)
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

        public async Task<ToDoListEntity> Update(int id, ToDoListEntity toDodb)
        {
            return await _toDo.EditToDoById(id, toDodb);
        }

        public async Task<ToDoListEntity> Create(ToDoListEntity toDodb)
        {
            return await _toDo.CreateToDoItem(toDodb);
        }

        public async Task DeleteAsync(int id)
        {
            await _toDo.DeleteToDoById(id);
        }

    }

}
