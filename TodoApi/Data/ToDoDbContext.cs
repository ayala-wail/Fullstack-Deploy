using Microsoft.EntityFrameworkCore;
using TodoApi.Models; // אם Item.cs נמצא בתיקיית Models

namespace TodoApi.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
    }
}
