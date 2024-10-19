using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Page
{
    public long Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public long? CreatedByAdminId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual User? CreatedByAdmin { get; set; }
}
