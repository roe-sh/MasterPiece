namespace LoveSeedM.DTOs
{
    public class UpdateCartDTO
    {
        public int? UserId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
