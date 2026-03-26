using System;
using System.Collections.Generic;

namespace Logorhythms.Backend.Models;

public partial class Document
{
    public int DocId { get; set; }

    public string Title { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public DateOnly UploadDate { get; set; }

    public DateTime? CreatedAt { get; set; }
}
