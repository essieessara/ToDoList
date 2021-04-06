using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions
{
    public class ToDoAlreadyExistsException : ToDoExceptions
    {
        public ToDoAlreadyExistsException()
        {
            base.Errors.Add("An item with the same name already exists");
        }
    }
}
