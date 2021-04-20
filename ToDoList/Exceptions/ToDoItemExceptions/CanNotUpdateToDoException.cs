using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions.ToDoItemExceptions
{
    public class CanNotUpdateToDoException : ToDoExceptions
    {
        public CanNotUpdateToDoException()
        {
            Errors.Add("Can Not Update The Status After Closing It");
        }
    }
}
