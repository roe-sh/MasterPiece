using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class Translation
{
    public long Id { get; set; }

    public long? EntityId { get; set; }

    public string? EntityType { get; set; }

    public long? LanguageId { get; set; }

    public string? Field { get; set; }

    public string? TranslatedText { get; set; }

    public virtual Language? Language { get; set; }
}
