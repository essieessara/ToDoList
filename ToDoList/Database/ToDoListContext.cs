using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Repositories
{
    public class ToDoListContext:DbContext
    {
        public DbSet<ToDoListEntity> Lists { get; set; }
        public ToDoListContext (DbContextOptions<ToDoListContext> options)
        : base(options)
        { 
        }
    }
}
