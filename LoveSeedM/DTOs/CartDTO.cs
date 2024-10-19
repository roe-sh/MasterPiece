using LoveSeedM.DTOs;

public class CartDTO
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal? Price { get; set; }   // Price of the cart item

    public ProductDTO Product { get; set; }  // Corrected property name to PascalCase
}
