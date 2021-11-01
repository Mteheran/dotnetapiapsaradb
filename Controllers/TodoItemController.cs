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