﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions
{
    public class ToDoValueIsNullException : ToDoExceptions
    {
        public ToDoValueIsNullException()
        {
            base.Errors.Add("ToDo body is Empty");
        }
    }
}
