namespace LoveSeedM.DTOs
{
    public class UpdateHealthTipDTO
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public bool? IsActive { get; set; }
    }
}
