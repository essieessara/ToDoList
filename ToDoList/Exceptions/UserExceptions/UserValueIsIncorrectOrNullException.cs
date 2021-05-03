namespace ToDoList.Exceptions.UserExceptions
{
    public class UserValueIsIncorrectOrNullException : ToDoExceptions
    {
        public UserValueIsIncorrectOrNullException()
        {
            Errors.Add("Make sure all user field are not empty");
            Errors.Add("Make sure all user field are correct");
        }
    }
}
