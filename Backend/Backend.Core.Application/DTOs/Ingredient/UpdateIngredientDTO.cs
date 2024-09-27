using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Application.DTOs.Ingredient
{
    public class UpdateIngredientDTO
    {
        [Required]
        [MaxLength(300, ErrorMessage = "Name max length is 300 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = "Description max length is 1000 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public int Stock { get; set; }
    }
}
