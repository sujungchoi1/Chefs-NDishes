using Microsoft.EntityFrameworkCore;
namespace ChefAndDishes.Models
{
    public class ChefDishContext : DbContext
    {
        public ChefDishContext(DbContextOptions options) : base(options) { }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Chef> Chefs { get; set; }
    }
}