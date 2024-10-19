using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class BabyBmirecord
{
    public long Id { get; set; }

    public long? MotherId { get; set; }

    public string? BabyName { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Bmi { get; set; }

    public DateTime? RecordDate { get; set; }

    public virtual User? Mother { get; set; }
}
