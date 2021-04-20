namespace ToDoList.Exceptions.UserExceptions
{
    public class UserAlreadyExistsException : ToDoExceptions

    {
        public UserAlreadyExistsException()
        {
            Errors.Add("A User with the same username already exists");
        }
    }
}
