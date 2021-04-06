using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Exceptions
{
    public class ResponseError
    {
        public List<string> Errors { get; set; }
        public bool Success { get; set; } = false;
        public ResponseError(List<string> error)
        {
            Errors = error;
        }
    }
}
