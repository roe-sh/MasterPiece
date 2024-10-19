namespace LoveSeedM.DTOs
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }        // Product ID
        public string ProductName { get; set; }   // Product name
        public int Quantity { get; set; }         // Quantity ordered
        public decimal Price { get; set; }        // Price of the product at the time of the order
    }
}
