using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Appointment
{
    public long Id { get; set; }

    public int? DoctorId { get; set; }

    public int? UserId { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public string? Notes { get; set; }

    public bool? IsConfirmed { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? AppointmentType { get; set; }

    public string? Status { get; set; }

    public string? Notesq { get; set; }

    public long? AdminHandledById { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual User? User { get; set; }
}
