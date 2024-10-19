using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Coupon
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public long? CreatedByAdminId { get; set; }

    public virtual User? CreatedByAdmin { get; set; }
}
