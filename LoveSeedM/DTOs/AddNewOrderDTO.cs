namespace LoveSeedM.DTOs
{
    public class AddNewOrderDTO
    {
        public int UserId { get; set; }           // User's ID
        public int ProductId { get; set; }        // Product's ID being ordered
        public int Quantity { get; set; }         // Quantity of the product
        public decimal TotalAmount { get; set; }  // Total amount for the order
    }
}
