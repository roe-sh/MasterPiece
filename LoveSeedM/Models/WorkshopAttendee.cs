using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class WorkshopAttendee
{
    public long Id { get; set; }

    public long? WorkshopId { get; set; }

    public long? UserId { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public string? Status { get; set; }

    public virtual User? User { get; set; }

    public virtual Workshop? Workshop { get; set; }
}
