using System;

namespace ToDoList.Models.ResponseModels
{
    public class ToDoItemResponseModel
    {

        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public bool IsFinished { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndedDate { get; set; }

        public UserDataResponseModel UserData { get; set; }
    }
}
