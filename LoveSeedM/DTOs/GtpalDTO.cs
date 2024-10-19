namespace LoveSeedM.DTOs
{
    public class GtpalDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }  // The ID of the associated user
        public int? Gravida { get; set; } // Number of pregnancies
        public int? Term { get; set; }    // Number of full-term pregnancies
        public int? Preterm { get; set; } // Number of preterm pregnancies
        public int? Abortions { get; set; } // Number of abortions or miscarriages
        public int? LivingChildren { get; set; } // Number of living children
        public DateTime? RecordDate { get; set; }  // Date when the record was added
    }
}
