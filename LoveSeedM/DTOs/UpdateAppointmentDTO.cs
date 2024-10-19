namespace LoveSeedM.DTOs
{

    public class UpdateAppointmentDTO
    {
        public DateTime? AppointmentDate { get; set; }
        public string? Notes { get; set; }
        public bool? IsConfirmed { get; set; }
    }
}
