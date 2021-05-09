namespace ToDoList.Exceptions.UserExceptions
{
    public class UserNotLoggedInException : ToDoExceptions
    {
        public UserNotLoggedInException()
        {
            Errors.Add("You need to LogIn");
        }
    }
}

