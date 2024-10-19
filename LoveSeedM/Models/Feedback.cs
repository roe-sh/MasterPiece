using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? DoctorId { get; set; }

    public int? ServiceId { get; set; }

    public int? Rating { get; set; }

    public string? Comments { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Service? Service { get; set; }

    public virtual User? User { get; set; }
}
