using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class FavoriteProduct
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public long? ProductId { get; set; }

    public DateTime? AddedDate { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
