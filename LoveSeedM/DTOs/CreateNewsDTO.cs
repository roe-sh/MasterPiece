using Microsoft.AspNetCore.Http;

namespace LoveSeedM.DTOs
{
    public class CreateNewsDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile? Image { get; set; }  // Image file uploaded
        public int? CreatedById { get; set; }  // ID of the user who created the news
        public string? Status { get; set; }
    }
}
