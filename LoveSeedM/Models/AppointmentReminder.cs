using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class AppointmentReminder
{
    public long Id { get; set; }

    public long? AppointmentId { get; set; }

    public DateTime? ReminderDate { get; set; }

    public bool? IsSent { get; set; }

    public virtual Appointment? Appointment { get; set; }
}
