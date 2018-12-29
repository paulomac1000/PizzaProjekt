using PizzaProjekt.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PizzaProjekt.ViewModels
{
    public class PizzaViewModel
    {
        public PizzaViewModel()
        {
        }

        public PizzaViewModel(Pizza pizza)
        {
            Id = pizza.Id;
            Name = pizza.Name;
            IngredientsIds = pizza.Ingredients.Select(i => i.Id).ToList(); ;
            Versions = pizza.Versions.Select(v => new VersionViewModel()
            {
                Batter = v.Batter,
                Size = v.Size,
                Price = v.Price
            }).ToList();
        }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(25)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        public ICollection<int> IngredientsIds { get; set; }

        public ICollection<VersionViewModel> Versions { get; set; }
    }
}