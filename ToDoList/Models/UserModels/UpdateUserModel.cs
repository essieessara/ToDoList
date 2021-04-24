using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.UserModels
{
    public class UpdateUserModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
