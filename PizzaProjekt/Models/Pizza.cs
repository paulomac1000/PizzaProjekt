using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaProjekt.Models
{
    public class Pizza
    {
        public Pizza()
        {
            Versions = new HashSet<Version>();
            Ingredients = new HashSet<Ingredient>();
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

        [Display(Name = "Rodzaje")]
        public virtual ICollection<Version> Versions { get; set; }

        [Display(Name = "Składniki")]
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}