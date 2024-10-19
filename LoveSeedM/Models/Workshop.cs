using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Workshop
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime WorkshopDate { get; set; }

    public string? Duration { get; set; }

    public int? Capacity { get; set; }

    public string? Location { get; set; }

    public long? CreatedByAdminId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Image { get; set; }

    public decimal? Price { get; set; }

    public virtual User? CreatedByAdmin { get; set; }

    public virtual ICollection<WorkshopAttendee> WorkshopAttendees { get; set; } = new List<WorkshopAttendee>();
}
