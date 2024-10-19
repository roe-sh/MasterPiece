using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Gtpal
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? Gravida { get; set; }

    public int? Term { get; set; }

    public int? Preterm { get; set; }

    public int? Abortions { get; set; }

    public int? LivingChildren { get; set; }

    public DateTime? RecordDate { get; set; }

    public virtual User? User { get; set; }
}
