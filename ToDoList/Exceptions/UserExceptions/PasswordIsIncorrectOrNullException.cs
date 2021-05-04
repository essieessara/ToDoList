using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions.UserExceptions
{
    public class PasswordIsIncorrectOrNullException : ToDoExceptions
    {
        public PasswordIsIncorrectOrNullException()
        {
            Errors.Add("Make sure you entered a valid password");
        }
    }
}
