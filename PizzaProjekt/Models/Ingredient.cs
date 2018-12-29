using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaProjekt.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            Pizzas = new HashSet<Pizza>();
        }

        [Key]
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(25)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}