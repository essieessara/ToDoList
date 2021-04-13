namespace ToDoList.Exceptions
{
    public class ToDoAlreadyExistsException : ToDoExceptions
    {
        public ToDoAlreadyExistsException()
        {
            base.Errors.Add("An item with the same name already exists");
        }
    }
}
