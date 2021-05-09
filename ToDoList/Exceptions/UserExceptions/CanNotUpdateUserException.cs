namespace ToDoList.Exceptions.UserExceptions
{
    public class CanNotUpdateUserException : ToDoExceptions
    {
        public CanNotUpdateUserException()
        {
            Errors.Add("Make Sure You Entered the right user credentials");
            Errors.Add("Make Sure All fields are correct");
        }
    }
}
