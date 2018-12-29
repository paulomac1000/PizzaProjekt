namespace PizzaProjekt.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<PizzaProjekt.Models.Context.PizzaProjektContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PizzaProjekt.Models.Context.PizzaProjektContext context)
        {
        }
    }
}