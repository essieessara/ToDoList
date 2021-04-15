using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions
{
    public class ToDoUsersExceptions : Exception
    {
        public ToDoUsersExceptions()
        {
        }
        public List<string> Errors { get; set; } = new();

        public ToDoUsersExceptions(string messages) : base(messages)
        {
            Errors.Add(messages);
        }

        public ToDoUsersExceptions(string[] messages)
        {
            Errors.AddRange(messages);
        }

        
    }
}
