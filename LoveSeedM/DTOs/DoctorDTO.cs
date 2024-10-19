namespace LoveSeedM.DTOs
{
    // DTO for returning doctor details
    public class DoctorDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Clinic { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public List<FeedbackDTO> Feedbacks { get; set; }
    }

   

}
