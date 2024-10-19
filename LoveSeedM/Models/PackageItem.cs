using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class PackageItem
{
    public long Id { get; set; }

    public long? PackageId { get; set; }

    public long? ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual Package? Package { get; set; }

    public virtual Product? Product { get; set; }
}
