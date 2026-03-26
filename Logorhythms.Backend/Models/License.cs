using System;
using System.Collections.Generic;

namespace Logorhythms.Backend.Models;

public partial class License
{
    public int LicenseId { get; set; }

    public int? ApplicationId { get; set; }

    public int UserId { get; set; }

    public string LicenseNumber { get; set; } = null!;

    public int LicenseTypeId { get; set; }

    public DateOnly IssueDate { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
