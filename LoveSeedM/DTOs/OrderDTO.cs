namespace LoveSeedM.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }                     // Order ID
        public DateTime? OrderDate { get; set; }        // Order date
        public decimal TotalAmount { get; set; }        // Total amount
        public string? Status { get; set; }             // Order status
        public string? TransactionId { get; set; }      // External transaction ID

        // Add this property to handle the list of order items
        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();

        // User details related to the order
        public UserOrderDTO UserOrderDTO { get; set; }

        // Voucher details (if applicable)
        public VoucherOrderDTO Vouchers { get; set; }
    }
}
