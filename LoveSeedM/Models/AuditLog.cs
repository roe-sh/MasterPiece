using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class AuditLog
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public string? Action { get; set; }

    public string? EntityType { get; set; }

    public long? EntityId { get; set; }

    public string? ChangeDetails { get; set; }

    public DateTime? ChangeDate { get; set; }

    public virtual User? User { get; set; }
}
