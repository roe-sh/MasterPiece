namespace LoveSeedM.DTOs
{
   
        public class CreateAppointmentDTO
        {
            public int? DoctorId { get; set; }
            public int? UserId { get; set; }
            public DateTime? AppointmentDate { get; set; }
            public string? Notes { get; set; }
        }
    }

