using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions
{
    public class ToDoUserNotFoundException : ToDoExceptions
    {
        public ToDoUserNotFoundException()
        {
            base.Errors.Add("no users where found with this ID");
            base.Errors.Add("make sure you entered the right ID");
        }
    }
}
