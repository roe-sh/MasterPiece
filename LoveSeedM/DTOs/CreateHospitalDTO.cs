namespace LoveSeedM.DTOs
{
    public class CreateHospitalDTO
    {
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
