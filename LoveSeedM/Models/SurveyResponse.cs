using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class SurveyResponse
{
    public long Id { get; set; }

    public long? SurveyId { get; set; }

    public long? UserId { get; set; }

    public string? ResponseText { get; set; }

    public DateTime? ResponseDate { get; set; }

    public virtual Survey? Survey { get; set; }

    public virtual User? User { get; set; }
}
