using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions
{
    public class CanNotUpdateException : ToDoExceptions
    {
        public CanNotUpdateException()
        {
            base.Errors.Add("Can Not Update The Status After Closing It");
        }
    }
}
