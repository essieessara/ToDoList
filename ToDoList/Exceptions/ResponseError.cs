using System.Collections.Generic;

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
