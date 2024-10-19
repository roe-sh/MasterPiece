namespace LoveSeedM.DTOs
{
    public class PaymentsDTO
    {
        public decimal PaymentAmount { get; set; }
        public string? PaymentMethod { get; set; }  // Optional, default to "Paypal"
        public string? TransactionId { get; set; }  // Optional, will be generated if null
        public string? PaymentStatus { get; set; }  // Optional, default to "Completed"
    }
}
