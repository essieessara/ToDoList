namespace ToDoList.Exceptions.ToDoItemExceptions
{
    public class CanNotUpdateToDoException : ToDoExceptions
    {
        public CanNotUpdateToDoException()
        {
            Errors.Add("Can Not Update The Status After Closing It");
        }
    }
}
