namespace LoveSeedM.DTOs
{
    public class CreateHealthTipDTO
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int? CreatedById { get; set; }
        public bool? IsActive { get; set; }
    }
}
