using System;
using System.Collections.Generic;

namespace Logorhythms.Backend.Models;

public partial class LicenseApplication
{
    public int ApplicationId { get; set; }

    public int UserId { get; set; }

    public int LicenseTypeId { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly ApplicationDate { get; set; }

    public string? SubmittedDocuments { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual LicenseType LicenseType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
