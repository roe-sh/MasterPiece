using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Language
{
    public long Id { get; set; }

    public string? LanguageCode { get; set; }

    public string? LanguageName { get; set; }

    public virtual ICollection<Translation> Translations { get; set; } = new List<Translation>();
}
