using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Database
{
    public class ToDoItemtEntity
    {
        [Key]
        public int ItemID { get; set; }
        [Required]
        public string ItemName { get; set; }
        [DefaultValue("false")]
        public bool IsFinished { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndedDate { get; set; }
        public UserEntity User { get; set; }
        public int UserID { get; set; }

    }
}
