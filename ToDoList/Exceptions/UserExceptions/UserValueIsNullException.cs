using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions.UserExceptions
{
    public class UserValueIsNullException : ToDoExceptions
    {
        public UserValueIsNullException()
        {
            Errors.Add("Make sure all user field are not empty");
        }
    }
}
