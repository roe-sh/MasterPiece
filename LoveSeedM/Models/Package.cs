using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Package
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? TotalPrice { get; set; }

    public long? DiscountId { get; set; }

    public long? CreatedByAdminId { get; set; }

    public virtual User? CreatedByAdmin { get; set; }

    public virtual Discount? Discount { get; set; }

    public virtual ICollection<PackageItem> PackageItems { get; set; } = new List<PackageItem>();
}
