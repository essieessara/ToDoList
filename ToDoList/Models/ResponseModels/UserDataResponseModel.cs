using Newtonsoft.Json;
using System.Collections.Generic;

namespace ToDoList.Models.ResponseModels
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class UserDataResponseModel
    {

        public int UserID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string Username { get; set; }

        public virtual ICollection<ToDoItemResponseModel> ToDoLists { get; set; }

    }
}
