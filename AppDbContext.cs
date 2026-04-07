using Microsoft.EntityFrameworkCore;

namespace P3
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Register> Users { get; set; }
        // Конфигурация подключения к базе данных SQLite с указанием файла базы данных
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
    }
}