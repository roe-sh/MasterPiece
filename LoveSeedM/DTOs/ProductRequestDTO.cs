using System.ComponentModel.DataAnnotations;

namespace LoveSeedM.DTOs
{
    public class ProductRequestDTO
    {
        [Required]
        public string ProductName { get; set; }   // Name of the product

        public string Description { get; set; }   // Description of the product

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }        // Price of the product

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Stock quantity must be at least 1")]
        public int StockQuantity { get; set; }    // Available stock quantity

        public decimal? Discount { get; set; }    // Discount on the product (if any)

        [Required]
        public int CategoryId { get; set; }       // ID of the category the product belongs to

        [Required]
        public IFormFile Image { get; set; }      // The image file for the product
    }
}

