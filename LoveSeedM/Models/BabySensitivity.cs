using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class BabySensitivity
{
    public long Id { get; set; }

    public long? MotherId { get; set; }

    public string? BabyName { get; set; }

    public string? SensitivityType { get; set; }

    public DateTime? RecordDate { get; set; }

    public virtual User? Mother { get; set; }
}
