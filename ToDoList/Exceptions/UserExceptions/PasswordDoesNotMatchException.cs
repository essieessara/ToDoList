using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions.UserExceptions
{
    public class PasswordDoesNotMatchException : ToDoExceptions
    {
        public PasswordDoesNotMatchException()
        {
            Errors.Add("Password Does Not Match");
            Errors.Add("Make Sure Right Password Is Entered");
        }
    }
}
