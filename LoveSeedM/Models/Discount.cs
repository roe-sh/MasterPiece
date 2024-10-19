using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Discount
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public long? CreatedByAdminId { get; set; }

    public virtual User? CreatedByAdmin { get; set; }

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();
}
