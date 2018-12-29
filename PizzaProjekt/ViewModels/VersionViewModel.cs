using PizzaProjekt.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PizzaProjekt.ViewModels
{
    public class VersionViewModel
    {
        public VersionViewModel()
        {
        }

        [Required]
        [Range(1, double.MaxValue)]
        [Display(Name = "Cena")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Rozmiar")]
        public Size Size { get; set; }

        [Required]
        [Display(Name = "Ciasto")]
        public Batter Batter { get; set; }
    }
}