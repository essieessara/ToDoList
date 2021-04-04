using System.ComponentModel.DataAnnotations;

namespace ToDoList.Database
{
    public class ToDoListEntity
    {
        [Key]
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public bool IsFinished { get; set; }
    }
}
