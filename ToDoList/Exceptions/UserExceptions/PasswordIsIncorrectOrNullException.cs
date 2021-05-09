namespace ToDoList.Exceptions.UserExceptions
{
    public class PasswordIsIncorrectOrNullException : ToDoExceptions
    {
        public PasswordIsIncorrectOrNullException()
        {
            Errors.Add("Make sure you entered a valid password");
        }
    }
}
