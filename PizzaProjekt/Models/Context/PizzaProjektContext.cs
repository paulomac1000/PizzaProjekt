using System.Data.Entity;

namespace PizzaProjekt.Models.Context
{
    public class PizzaProjektContext : DbContext
    {
        public PizzaProjektContext() : base("PizzaProjektConnection")
        {
        }

        public static PizzaProjektContext Create()
        {
            return new PizzaProjektContext();
        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}