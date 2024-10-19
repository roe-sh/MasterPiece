using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class SurveyQuestion
{
    public long Id { get; set; }

    public long? SurveyId { get; set; }

    public string? QuestionText { get; set; }

    public virtual Survey? Survey { get; set; }
}
