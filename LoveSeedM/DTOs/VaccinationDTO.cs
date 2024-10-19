namespace LoveSeedM.DTOs
{
    public class VaccinationDTO
    {
        public int Id { get; set; }
        public int? KidId { get; set; }
        public string VaccineName { get; set; } = null!;
        public DateTime VaccinationDate { get; set; }
        public DateTime? NextDoseDate { get; set; }
        public int? AdministeredById { get; set; }
        public string? AdministeredByName { get; set; }
    }
}
