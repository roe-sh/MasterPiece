using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Hospital
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }
}
