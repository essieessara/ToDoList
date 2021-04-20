﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions.UserExceptions
{
    public class UserNotFoundException : ToDoExceptions
    {
        public UserNotFoundException()
        {
            Errors.Add("no users where found with this ID");
            Errors.Add("make sure you entered the right ID");
        }
    }
}
