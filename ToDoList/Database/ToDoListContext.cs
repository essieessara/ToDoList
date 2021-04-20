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


        public DbSet<ToDoItemtEntity> Lists { get; set; }
        public DbSet<UserEntity> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ToDoItemtEntity>()
                .HasOne(x => x.User)
                .WithMany(x => x.Lists)
                .HasForeignKey(x => x.UserID);




        }


    }

}

