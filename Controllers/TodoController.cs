using FinalTodoApi.Controllers.Models;
using FinalTodoApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController : Controller
    {
        private readonly TodosAPIDbContext dbContext;

        public TodosController(TodosAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            return Ok(await dbContext.Todos.ToListAsync());
            // return View();
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetTodo([FromRoute] Guid id)
        {
            var todo = await dbContext.Todos.FindAsync(id);
            if (todo != null)
            {
                return Ok(todo);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(AddTodoRequest addTodoRequest)
        {
            var todo = new Todo()
            {
                Id = Guid.NewGuid(),
                Title = addTodoRequest.Title,
                Description = addTodoRequest.Description,
                isComplete = false
            };

            await dbContext.Todos.AddAsync(todo);
            await dbContext.SaveChangesAsync();
            return Ok(todo);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] Guid id, UpdateTodoRequest updateTodoRequest)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            if (todo != null)
            {
                todo.Title = updateTodoRequest.Title;
                todo.Description = updateTodoRequest.Description;
                todo.isComplete = updateTodoRequest.isComplete;

                await dbContext.SaveChangesAsync();
                return Ok(todo);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
        {
            var todo = await dbContext.Todos.FindAsync(id);
            if (todo != null)
            {
                dbContext.Remove(todo);
                await dbContext.SaveChangesAsync();
                return Ok(todo);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
