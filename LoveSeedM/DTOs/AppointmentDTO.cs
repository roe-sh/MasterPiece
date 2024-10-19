namespace LoveSeedM.DTOs
{
    public class AppointmentDTO
    {
        public long Id { get; set; }
        public int? DoctorId { get; set; }
        public int? UserId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Notes { get; set; }
        public bool? IsConfirmed { get; set; }
    }

   

}
