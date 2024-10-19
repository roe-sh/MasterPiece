using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Clinic { get; set; }

    public string? Specialization { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual User? User { get; set; }
}
