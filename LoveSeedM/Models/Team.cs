using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Team
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }

    public string? Bio { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Image { get; set; }

    public string? SocialMedia { get; set; }

    public DateTime? JoinedDate { get; set; }

    public bool? IsActive { get; set; }
}
