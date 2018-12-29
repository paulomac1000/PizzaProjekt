using PizzaProjekt.Models;
using System.ComponentModel.DataAnnotations;

namespace PizzaProjekt.ViewModels
{
    public class IngredientViewModel
    {
        public IngredientViewModel()
        {
        }

        public IngredientViewModel(Ingredient ingredient)
        {
            Id = ingredient.Id;
            Name = ingredient.Name;
        }

        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(25)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Używany w liczbie pizz")]
        public int UsedInPizzasCount { get; set; }

        [Display(Name = "Może być usunięty?")]
        public bool CanBeDeleted => UsedInPizzasCount <= 0;
    }
}