using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions
{
    public class ToDoNotFoundException : ToDoExceptions
    {
        public ToDoNotFoundException()
        {
            base.Errors.Add("no itmes where found with this ID");
            base.Errors.Add("make sure you entered the right ID");
        }
    }
}
