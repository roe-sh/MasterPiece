﻿using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class News
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Image { get; set; }

    public DateTime? PublishedDate { get; set; }

    public int? CreatedById { get; set; }

    public string? Status { get; set; }

    public virtual User? CreatedBy { get; set; }
}
