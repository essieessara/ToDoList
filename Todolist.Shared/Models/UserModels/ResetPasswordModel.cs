using System.ComponentModel.DataAnnotations;

namespace Todolist.Shared.Models.UserModels
{
    public class ResetPasswordModel
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }

}

