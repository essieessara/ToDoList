namespace ToDoList.Exceptions.UserExceptions
{
    public class UserValueIsNullException : ToDoExceptions
    {
        public UserValueIsNullException()
        {
            Errors.Add("Make sure all user field are not empty");
        }
    }
}
