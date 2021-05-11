﻿using System.ComponentModel.DataAnnotations;

namespace Todolist.Shared.Models.UserModels
{
    public class ResetPasswordModel
    {
        //public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }

}

