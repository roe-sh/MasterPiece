namespace LoveSeedM.DTOs
{
    public class NewsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? Image { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }  // Username of the creator
    }
}
