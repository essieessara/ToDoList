using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions.UserExceptions
{
    public class UserNotLoggedInException : ToDoExceptions
    {
        public UserNotLoggedInException()
        {
            Errors.Add("You need to LogIn");
        }
    }
}

