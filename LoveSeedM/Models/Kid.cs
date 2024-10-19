using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Kid
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public int? FormulaId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual MilkFormula? Formula { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
