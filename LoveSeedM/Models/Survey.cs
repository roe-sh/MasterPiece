using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Survey
{
    public long Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public long? CreatedByAdminId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual User? CreatedByAdmin { get; set; }

    public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; } = new List<SurveyQuestion>();

    public virtual ICollection<SurveyResponse> SurveyResponses { get; set; } = new List<SurveyResponse>();
}
