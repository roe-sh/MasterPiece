using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Point
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public int? PointsEarned { get; set; }

    public int? PointsSpent { get; set; }

    public string? Activity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual User? User { get; set; }
}
