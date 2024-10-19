using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class ContactU
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Message { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Subject { get; set; }

    public string? Email { get; set; }

    public string? ReplyMessage { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
