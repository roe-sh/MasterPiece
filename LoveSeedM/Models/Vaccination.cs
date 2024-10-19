using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Vaccination
{
    public int Id { get; set; }

    public int? KidId { get; set; }

    public string VaccineName { get; set; } = null!;

    public DateTime VaccinationDate { get; set; }

    public DateTime? NextDoseDate { get; set; }

    public int? AdministeredById { get; set; }

    public virtual User? AdministeredBy { get; set; }

    public virtual Kid? Kid { get; set; }
}
