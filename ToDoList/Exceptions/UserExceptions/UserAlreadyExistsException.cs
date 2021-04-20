﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions.UserExceptions
{
    public class UserAlreadyExistsException : ToDoExceptions

    {
        public UserAlreadyExistsException()
        {
            Errors.Add("A User with the same username already exists");
        }
    }
}
