using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions
{
    public class ToDoUserAlreadyExistsException : ToDoExceptions

    {
        public ToDoUserAlreadyExistsException()
        {
            base.Errors.Add("A User with the same username already exists");
        }
    }
}
