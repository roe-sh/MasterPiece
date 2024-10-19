using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class MilkFormula
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? MinAgeMonths { get; set; }

    public int? MaxAgeMonths { get; set; }

    public bool? SuitableForSensitive { get; set; }

    public int? CreatedById { get; set; }

    public virtual User? CreatedBy { get; set; }

    public virtual ICollection<Kid> Kids { get; set; } = new List<Kid>();
}
