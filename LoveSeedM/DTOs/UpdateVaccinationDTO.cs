namespace LoveSeedM.DTOs
{
    public class UpdateVaccinationDTO
    {
        public string VaccineName { get; set; } = null!;
        public DateTime VaccinationDate { get; set; }
        public DateTime? NextDoseDate { get; set; }
    }
}
