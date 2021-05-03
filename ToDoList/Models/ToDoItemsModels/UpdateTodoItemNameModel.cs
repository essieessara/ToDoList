namespace ToDoList.Models.ToDoItemsModels
{
    public class UpdateTodoItemNameModel
    {
        public int ItemID { get; set; }
        public int UserID { get; set; }
        public string ItemName { get; set; }
    }
}
