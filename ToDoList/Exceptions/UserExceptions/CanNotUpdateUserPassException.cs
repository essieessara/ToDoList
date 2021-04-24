using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions.UserExceptions
{
    public class CanNotUpdateUserPassException : ToDoExceptions
    {
        public CanNotUpdateUserPassException()
        {
            Errors.Add("Make Sure You Entered the right Password");
            Errors.Add("Make Sure Passwords are the same");
        }
    }
}
