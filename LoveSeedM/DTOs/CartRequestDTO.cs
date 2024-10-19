namespace LoveSeedM.DTOs
{
    public class CartRequestDTO
    {
        public int UserId { get; set; }        // User's ID
        public int ProductId { get; set; }     // Product's ID
        public int Quantity { get; set; }      // Quantity of product in the cart
        public ProductDTO ProductDTO { get; set; }  // Associated product details
    }
}
