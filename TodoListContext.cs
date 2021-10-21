using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiPostgre;
public class TodoListContext : DbContext
{
    
        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoListContext(DbContextOptions<TodoListContext> options) : base(options) 
        {
        }
        
}

