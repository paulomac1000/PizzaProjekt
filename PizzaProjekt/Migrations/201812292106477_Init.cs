namespace PizzaProjekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pizzas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Versions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        Size = c.Int(nullable: false),
                        Batter = c.Int(nullable: false),
                        Pizza_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pizzas", t => t.Pizza_Id, cascadeDelete: true)
                .Index(t => t.Pizza_Id);
            
            CreateTable(
                "dbo.PizzaIngredients",
                c => new
                    {
                        Pizza_Id = c.Int(nullable: false),
                        Ingredient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pizza_Id, t.Ingredient_Id })
                .ForeignKey("dbo.Pizzas", t => t.Pizza_Id, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_Id, cascadeDelete: true)
                .Index(t => t.Pizza_Id)
                .Index(t => t.Ingredient_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Versions", "Pizza_Id", "dbo.Pizzas");
            DropForeignKey("dbo.PizzaIngredients", "Ingredient_Id", "dbo.Ingredients");
            DropForeignKey("dbo.PizzaIngredients", "Pizza_Id", "dbo.Pizzas");
            DropIndex("dbo.PizzaIngredients", new[] { "Ingredient_Id" });
            DropIndex("dbo.PizzaIngredients", new[] { "Pizza_Id" });
            DropIndex("dbo.Versions", new[] { "Pizza_Id" });
            DropTable("dbo.PizzaIngredients");
            DropTable("dbo.Versions");
            DropTable("dbo.Pizzas");
            DropTable("dbo.Ingredients");
        }
    }
}
