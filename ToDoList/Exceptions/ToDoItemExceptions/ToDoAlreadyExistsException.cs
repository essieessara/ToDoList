namespace ToDoList.Exceptions.ToDoItemExceptions
{
    public class ToDoAlreadyExistsException : ToDoExceptions
    {
        public ToDoAlreadyExistsException()
        {
            base.Errors.Add("An item with the same name already exists");
        }
    }
}
