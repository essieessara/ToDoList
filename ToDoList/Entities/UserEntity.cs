using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Database
{
    public class UserEntity
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public virtual ICollection<ToDoItemtEntity> Lists { get; set; }
    }
}
