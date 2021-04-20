namespace ToDoList.Exceptions.ToDoItemExceptions
{
    public class ToDoNotFoundException : ToDoExceptions
    {
        public ToDoNotFoundException()
        {
            base.Errors.Add("no itmes where found with this ID");
            base.Errors.Add("make sure you entered the right ID");
        }
    }
}
