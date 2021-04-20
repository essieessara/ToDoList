using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models.ResponseModels
{
    public class ToDoItemResponseModel
    {
       
        public int ItemID { get; set; }  
        public string ItemName { get; set; }
        public bool IsFinished { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndedDate { get; set; }
    }
}
