﻿using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? NotificationType { get; set; }

    public string? Message { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual User? User { get; set; }
}
