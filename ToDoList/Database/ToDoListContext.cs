using Microsoft.EntityFrameworkCore;
using ToDoList.Database;

namespace ToDoList.Repositories
{
    public class ToDoListContext : DbContext
    {
        public DbSet<ToDoListEntity> Lists { get; set; }
        public DbSet<ToDoUsersEntity> Users { get; set; }
        public ToDoListContext(DbContextOptions<ToDoListContext> options)
        : base(options)
        {
        }
    }
}
