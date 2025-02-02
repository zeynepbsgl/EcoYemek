

using Microsoft.EntityFrameworkCore;

namespace MealBox.Models.Classes
{
    public class Context : DbContext { 
        
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server= DESKTOP-520AD7C\\SQLEXPRESS; database = MealBoxDB;integrated security =true; TrustServerCertificate=True");
        }
    }

}
