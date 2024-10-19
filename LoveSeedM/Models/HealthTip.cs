using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class HealthTip
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int? CreatedById { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual User? CreatedBy { get; set; }
}
