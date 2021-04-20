using Newtonsoft.Json;
using System.Collections.Generic;

namespace ToDoList.Models.ResponseModels
{
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


        [JsonProperty(NullValueHandling= NullValueHandling.Ignore)]
        public virtual ICollection<ToDoItemResponseModel> ToDoLists { get; set; }
    }
}
