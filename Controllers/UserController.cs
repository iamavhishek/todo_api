using FinalTodoApi.Controllers.Models;
using FinalTodoApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalTodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly TodosAPIDbContext dbContext;

        public UserController(TodosAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] AddUserRequest user)
        {
            bool isValid = await dbContext.Users.AnyAsync(u => u.Email == user.Email && u.Password == user.Password);
            if (isValid)
            {
                return Ok("Valid Username and Password");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(AddUserRequest addUserRequest)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = addUserRequest.Email,
                Password = addUserRequest.Password,
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);
        }
    }
}
