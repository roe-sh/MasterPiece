namespace LoveSeedM.DTOs
{
    public class FeedbackDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
    }
}
