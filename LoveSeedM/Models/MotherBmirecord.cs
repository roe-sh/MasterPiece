using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class MotherBmirecord
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Bmi { get; set; }

    public DateTime? RecordDate { get; set; }

    public virtual User? User { get; set; }
}
