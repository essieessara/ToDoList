using Microsoft.EntityFrameworkCore;
using ToDoList.Database;

namespace ToDoList.Repositories
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext(DbContextOptions<ToDoListContext> options)
       : base(options)
        {
        }


        public DbSet<ToDoListEntity> Lists { get; set; }
        public DbSet<ToDoUsersEntity> Users { get; set; }
       
    }
}
