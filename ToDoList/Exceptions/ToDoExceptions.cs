using System;
using System.Collections.Generic;

namespace ToDoList.Exceptions
{
    public class ToDoExceptions : Exception
    {
        public List<string> Errors { get; set; } = new();
        public ToDoExceptions()
        {

        }
        public ToDoExceptions(string messages) : base(messages)
        {
            Errors.Add(messages);
        }

        public ToDoExceptions(string[] messages)
        {
            Errors.AddRange(messages);
        }
    }
}
