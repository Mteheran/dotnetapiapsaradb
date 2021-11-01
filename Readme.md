#dotnet API + ApsaraDB with postgreSQL

###This is a guide to create and API connected to ApsaraDB with postgreSQL

1. create the API using the command "dotnet new webapi"

2. Add the nugets for Entity framework and PostgreSQL

- dotnet add package Microsoft.EntityFrameworkCore
- dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

3. Create the class TodoItem

```csharp
namespace ApiPostgre;
public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}

```

4. Create TodoItemContext for Entity framework

```csharp
using Microsoft.EntityFrameworkCore;
namespace ApiPostgre;
public class TodoItemContext : DbContext
{    
        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoItemContext(DbContextOptions<TodoItemContext> options) : base(options) 
        {
        }        
}
```

5. Add TodoItemController including all method (get, post, put, delete)

```csharp
using Microsoft.AspNetCore.Mvc;
namespace ApiPostgre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        TodoItemContext bd;
        public TodoItemController(TodoItemContext context)
        {
            bd = context;
            bd.Database.EnsureCreated();
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            return  bd.TodoItems;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItemById(int id)
        {
            var currentItem = bd.TodoItems.FirstOrDefault(p=> p.Id == id);

            if(currentItem == null) return NotFound();

            return currentItem;
        }

        [HttpPost("")]
        public async Task PostTodoItem(TodoItem model)
        {
            bd.Add(model);
            await bd.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem model)
        {
            var currentItem = bd.TodoItems.FirstOrDefault(p=> p.Id == id);

            if(currentItem == null) return NotFound();

            currentItem.Title = model.Title;
            currentItem.IsCompleted = model.IsCompleted;
            await bd.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<TodoItem> DeleteTodoItemById(int id)
        {
            return null;
        }
    }
}
```

6. Create a new ApsaraDB RDS instance in Alibaba Cloud

![](images/AlibabaCloudRDS.png)
![](images/AlibabaCloudCreateInsance.png)
![](images/AlibabaCloudPosgrest.png)
![](images/AlibabaCloud20gb.png)
![](images/AlibabaCloudpaynow.png)

7. After waiting for the creation of the new instance go to ApsaraRDS instance and create a new account for this instance

![](images/AlibabaCLoudInstanses.png)
![](images/AlibabaCloudAccount.png)


8. Now create a new database and assing the account created before
   
![](images/AlibabaCloudCreateDatabase.png)


9. Finally, you can setup your IP or add 0.0.0.0/0 in the white list in order create an open access to this new database

![](images/AlibabaCloudIPsecurity.png)

10. Now, You can setup the PostgreSQL connetion and the EF service in your API go to (program.cs):

```csharp
// Add services to the container.
builder.Services.AddDbContext<TodoItemContext>(options => 
options.UseNpgsql("server=myserver.pg.rds.aliyuncs.com;database=todoitems;user id=user_1;password=mypassword")
);

```

11. Execute your API using "dotnet run" and it should works