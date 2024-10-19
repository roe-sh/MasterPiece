using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LoveSeedM.DTOs
{
    public class AddCategoryDTO
    {
        [Required]
        public string CategoryName { get; set; }  // Name of the category

        public string Description { get; set; }   // Description of the category

        [Required]
        public IFormFile Image { get; set; }      // The image file for the category
    }
}
