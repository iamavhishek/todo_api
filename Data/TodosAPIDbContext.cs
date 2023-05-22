using FinalTodoApi.Controllers.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalTodoApi.Data
{
    public class TodosAPIDbContext : DbContext
    {
        public TodosAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
