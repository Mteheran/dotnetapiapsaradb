using Microsoft.EntityFrameworkCore;
namespace ApiPostgre;
public class TodoItemContext : DbContext
{    
        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoItemContext(DbContextOptions<TodoItemContext> options) : base(options) 
        {
        }        
}

