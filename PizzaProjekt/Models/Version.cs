using PizzaProjekt.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaProjekt.Models
{
    public class Version
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Cena")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Rozmiar")]
        public Size Size { get; set; }

        [Required]
        [Display(Name = "Ciasto")]
        public Batter Batter { get; set; }

        [ForeignKey("Pizza_Id")]
        public virtual Pizza Pizza { get; set; }
        [Required]
        public int Pizza_Id { get; set; }
    }
}