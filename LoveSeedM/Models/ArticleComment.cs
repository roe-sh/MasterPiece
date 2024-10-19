using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class ArticleComment
{
    public long Id { get; set; }

    public long? ArticleId { get; set; }

    public long? UserId { get; set; }

    public string? Comment { get; set; }

    public DateTime? CommentDate { get; set; }

    public string? Status { get; set; }

    public virtual Article? Article { get; set; }

    public virtual User? User { get; set; }
}
