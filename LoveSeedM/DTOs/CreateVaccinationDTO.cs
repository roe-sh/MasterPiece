namespace LoveSeedM.DTOs
{
    public class CreateVaccinationDTO
    {
        public int? KidId { get; set; }
        public string VaccineName { get; set; } = null!;
        public DateTime VaccinationDate { get; set; }
        public DateTime? NextDoseDate { get; set; }
        public int? AdministeredById { get; set; }
    }
}
