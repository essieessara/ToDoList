namespace ToDoList.Exceptions.ToDoItemExceptions
{
    public class ToDoValueIsNullException : ToDoExceptions
    {
        public ToDoValueIsNullException()
        {
            base.Errors.Add("ToDo body is Empty");
        }
    }
}
