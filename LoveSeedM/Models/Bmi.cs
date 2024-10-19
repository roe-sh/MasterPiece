using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Bmi
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Bmi1 { get; set; }

    public DateTime? RecordDate { get; set; }

    public virtual User? User { get; set; }
}
