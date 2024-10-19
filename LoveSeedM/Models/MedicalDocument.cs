using System;
using System.Collections.Generic;

namespace LoveSeedM.Models;

public partial class MedicalDocument
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public string? DocumentType { get; set; }

    public DateTime? UploadedDate { get; set; }

    public virtual User? User { get; set; }
}
